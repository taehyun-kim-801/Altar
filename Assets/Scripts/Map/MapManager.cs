using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Transform player;

    public Tilemap tilemap;

    private Vector2 minTilemap;
    private Vector2 maxTilemap;

    private List<GameObject> monsterObjectPool;
    public int monsterCountSum { get; private set; }

    public int[] monstersCount { get; private set; }

    void Start()
    {
        minTilemap = tilemap.CellToWorld(tilemap.cellBounds.min);
        maxTilemap = tilemap.CellToWorld(tilemap.cellBounds.max);

        StartCoroutine(Spawn());

        monsterObjectPool = new List<GameObject>();

        monsterCountSum = 0;
        monstersCount = new int[3];

        SetObjectPool();
    }

    private void SetObjectPool()
    {
        foreach(var monster in MonsterManager.monstersByMap[SceneManager.GetActiveScene().name])
        {
            for(int i=0;i<10;i++)
            {
                GameObject spawnObj = new GameObject(monster);
                spawnObj.transform.localScale = new Vector2(5f, 5f);

                var monsterInfo = spawnObj.AddComponent<Monster>();
                MonsterInfo spawnInfo = MonsterManager.monsters[monster];

                monsterInfo.Health = spawnInfo.Health;
                monsterInfo.name = spawnInfo.Name;
                monsterInfo.damage = spawnInfo.Damage;
                monsterInfo.dropItem = spawnInfo.DropItem;
                monsterInfo.attackWaitSecond = 5f;

                monsterInfo.SetMaxHealth();

                var monsterRB = spawnObj.AddComponent<Rigidbody2D>();
                monsterRB.constraints = RigidbodyConstraints2D.FreezeRotation;
                monsterRB.gravityScale = 0f;

                var renderer = spawnObj.AddComponent<SpriteRenderer>();
                renderer.sprite = DataContainer.monsterAtlas.GetSprite($"{monster}_1");
                renderer.sortingOrder = 1;

                spawnObj.AddComponent<BoxCollider2D>();

                spawnObj.tag = "Monster";

                spawnObj.SetActive(false);

                monsterObjectPool.Add(spawnObj);
            }
        }
    }
    private IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitUntil(() => monsterCountSum < 10);
            
            float randX = Random.Range(-10.0f, 10.0f);
            float randY = Random.Range(-10.0f, 10.0f);
            int spawnIdx = Random.Range(0, MonsterManager.monstersByMap[SceneManager.GetActiveScene().name].Count);

            GameObject monster = monsterObjectPool[spawnIdx * 10 + monstersCount[spawnIdx]++];
            monster.transform.position = new Vector3(Mathf.Clamp(transform.position.x + randX, minTilemap.x, maxTilemap.x),
                Mathf.Clamp(transform.position.y + randY, minTilemap.y, maxTilemap.y));
            monsterCountSum++;
            monster.SetActive(true);

            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        }
    }

    public void DecreaseCount(string name)
    {
        monsterCountSum--;
        monstersCount[MonsterManager.monstersByMap[SceneManager.GetActiveScene().name].FindIndex(temp => temp == name)]--;
    }

    public void CheckPositionInTilemap(GameObject unit)
    {
        unit.transform.position = new Vector3(Mathf.Clamp(unit.transform.position.x, minTilemap.x, maxTilemap.x), Mathf.Clamp(unit.transform.position.y, minTilemap.y, maxTilemap.y));
    }
}
