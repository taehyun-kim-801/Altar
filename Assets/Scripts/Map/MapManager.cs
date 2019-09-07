using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    private Dictionary<string, List<string>> monstersByMap;
    private Dictionary<string, MonsterInfo> monsters;

    private Transform player;

    private SpriteAtlas atlas;
    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        monstersByMap = new Dictionary<string, List<string>>();
        monsters = new Dictionary<string, MonsterInfo>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(!File.Exists($"{Application.dataPath}/Data/MonsterInfo.json"))
        {
            FileStream fs = File.Create($"{Application.dataPath}/Data/MonsterInfo.json");
            fs.Close();
            JsonManager.SaveJson(SaveMonsters());
        }


        if(!File.Exists($"{Application.dataPath}/Data/MonsterList.json"))
        {
            FileStream fs = File.Create($"{Application.dataPath}/Data/MonsterList.json");
            fs.Close();
            JsonManager.SaveJson(SaveLists());
        }

        atlas = Resources.Load<SpriteAtlas>("Units");
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadMonstersJson();
        StartCoroutine(Spawn());
    }

    private void LoadMonstersJson()
    {
        JsonManager.LoadJson<MonsterInfo>().ForEach((monster) => { monsters.Add(monster.Name, monster); });

        foreach(var monster in monsters)
        {
            Debug.Log(monster.Key + " " + monster.Value);
        }

        JsonManager.LoadJson<MonsterList>().ForEach((list) => { monstersByMap.Add(list.name, list.monsters); });

        foreach (var list in monstersByMap)
        {
            Debug.Log(list.Key + " " + list.Value);
        }
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));

            float randX = Random.Range(-10.0f, 10.0f);
            float randY = Random.Range(-10.0f, 10.0f);
            int spawnIdx = Random.Range(1, monstersByMap[SceneManager.GetActiveScene().name].Count + 1);

            GameObject spawnObj = new GameObject(monstersByMap[SceneManager.GetActiveScene().name][spawnIdx - 1]);
            spawnObj.transform.localScale = new Vector2(5f, 5f);
            var monster = spawnObj.AddComponent<Monster>();
            spawnObj.tag = "Monster";
            BoxCollider2D collider = spawnObj.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(0.12f, 0.16f);
            Rigidbody2D monsterRB = spawnObj.AddComponent<Rigidbody2D>();
            monsterRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            monsterRB.gravityScale = 0f;
            SpriteRenderer render = spawnObj.AddComponent<SpriteRenderer>();

            render.sprite = atlas.GetSprite($"{monstersByMap[SceneManager.GetActiveScene().name][spawnIdx - 1]}_1");

            MonsterInfo spawnInfo = monsters[monstersByMap[SceneManager.GetActiveScene().name][spawnIdx - 1]];
            monster.Health = spawnInfo.Health;
            monster.name = spawnInfo.Name;
            monster.damage = spawnInfo.Damage;
            monster.dropItem = spawnInfo.DropItem;

            monster.attackWaitSecond = 5f;
            
            Instantiate(monster, new Vector3(player.position.x + randX, player.position.y + randY), new Quaternion(0, 0, 0, 0));
            Destroy(spawnObj);
        }
    }

    [System.Serializable]
    private struct MonsterList
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public List<string> monsters;

        public MonsterList(string name,List<string> monsters)
        {
            this.name = name;
            this.monsters = monsters;
        }
    }

    private List<MonsterInfo> SaveMonsters() 
    {
        List<MonsterInfo> result = new List<MonsterInfo>();

        result.Add(new MonsterInfo("Skeleton", 10, 1, "Bone"));
        result.Add(new MonsterInfo("Slime", 20, 2, "Jelly"));
        result.Add(new MonsterInfo("StoneGolem", 30, 3, "Stone"));

        return result;
    }

    private List<MonsterList> SaveLists()
    {
        List<MonsterList> result = new List<MonsterList>();

        result.Add(new MonsterList("Grave",new List<string> { "Skeleton","Slime","StoneGolem" }));
        return result;
    }
}
