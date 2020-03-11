using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public delegate void Func();

    public Transform player;

    private Tilemap tilemap;

    private Vector2 minTilemap;
    private Vector2 maxTilemap;

    private List<GameObject> monsterObjectPool;

    public string sceneName { get; private set; }
    public int monsterCountSum { get; private set; }

    public int[] monstersCount { get; private set; }

    public string nextScene;
    public bool isLoaded;

    public GameObject[] portalTransformList;

    void Start()
    {
        monsterObjectPool = new List<GameObject>();

        monsterCountSum = 0;
        monstersCount = new int[3];

        sceneName = SceneManager.GetActiveScene().name;

        var cellBounds = GameObject.FindGameObjectWithTag("CellBound");

        if (!(cellBounds is null))
        {
            minTilemap = cellBounds.transform.GetChild(0).position;
            maxTilemap = cellBounds.transform.GetChild(1).position;
        }

        SetObjectPool();

        if(sceneName != "Lobby")
            StartCoroutine(Spawn());
    }
    
    public IEnumerator ChangeScene(Func func = null)
    {
        StopAllCoroutines();

        isLoaded = false;

        yield return SceneManager.LoadSceneAsync(nextScene);

        isLoaded = true;

        MonsterCondition_UI.Instance.gameObject.SetActive(false);

        sceneName = SceneManager.GetActiveScene().name;
        var cellBounds = GameObject.FindGameObjectWithTag("CellBound");

        minTilemap = cellBounds.transform.GetChild(0).position;
        maxTilemap = cellBounds.transform.GetChild(1).position;

        monsterCountSum = 0;
        for (int i = 0; i < 3; i++)
        {
            monstersCount[i] = 0;
        }
        SetObjectPool();
        if (sceneName != "Lobby")
            StartCoroutine(Spawn());

        portalTransformList = GameObject.FindGameObjectsWithTag("Portal");

        Time.timeScale = 1f;

        func?.Invoke();
    }
    private void SetObjectPool()
    {
        monsterObjectPool.Clear();
        if (nextScene != "Lobby")
        {
            foreach (var monster in MonsterManager.monstersByMap[sceneName])
            {
                var monsterObject = Resources.Load("Monsters/"+monster) as GameObject;

                for (int i = 0; i < 10; i++)
                {
                    var spawnObj = Instantiate(monsterObject) as GameObject;
                    spawnObj.name = monster;
                    spawnObj.SetActive(false);

                    monsterObjectPool.Add(spawnObj);
                }
            }
        }
    }
    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
            yield return new WaitUntil(() => monsterCountSum < 10);

            float randX = Random.Range(-10.0f, 10.0f);
            float randY = Random.Range(-10.0f, 10.0f);
            int spawnIdx = Random.Range(0, MonsterManager.monstersByMap[sceneName].Count);

            while (monsterObjectPool[spawnIdx * 10 + monstersCount[spawnIdx]].activeSelf) { monstersCount[spawnIdx] = (monstersCount[spawnIdx] + 1) % 10; }

            GameObject monster = monsterObjectPool[spawnIdx * 10 + monstersCount[spawnIdx]++];
            monster.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x + randX, minTilemap.x, maxTilemap.x),
                Mathf.Clamp(player.transform.position.y + randY, minTilemap.y, maxTilemap.y));
            monsterCountSum++;
            monster.SetActive(true);
        }
    }

    public void DecreaseCount(string name)
    {
        monsterCountSum--;
    }

    public void CheckPositionInTilemap(GameObject unit)
    {
        var size = unit.GetComponent<SpriteRenderer>().sprite.bounds.size;

        unit.transform.position = new Vector3(Mathf.Clamp(unit.transform.position.x, minTilemap.x + size.x, maxTilemap.x - size.x), Mathf.Clamp(unit.transform.position.y, minTilemap.y + size.y, maxTilemap.y - size.y));
    }

    public void ResetObjectPool()
    {
        foreach(var monster in monsterObjectPool)
        {
            monster.SetActive(false);
            monsterCountSum = 0;
            for (int i = 0; i < monstersCount.Length; i++) monstersCount[i] = 0;
        }
    }
}
