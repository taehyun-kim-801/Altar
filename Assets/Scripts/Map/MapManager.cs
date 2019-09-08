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

    void Start()
    {
        minTilemap = tilemap.CellToWorld(tilemap.cellBounds.min);
        maxTilemap = tilemap.CellToWorld(tilemap.cellBounds.max);

        Debug.Log(minTilemap);
        Debug.Log(maxTilemap);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));

            float randX = Random.Range(-10.0f, 10.0f);
            float randY = Random.Range(-10.0f, 10.0f);
            int spawnIdx = Random.Range(1, MonsterManager.monstersByMap[SceneManager.GetActiveScene().name].Count + 1);

            GameObject spawnObj = new GameObject(MonsterManager.monstersByMap[SceneManager.GetActiveScene().name][spawnIdx - 1]);
            spawnObj.transform.localScale = new Vector2(5f, 5f);
            var monster = spawnObj.AddComponent<Monster>();
            spawnObj.tag = "Monster";
            BoxCollider2D collider = spawnObj.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(0.12f, 0.16f);
            Rigidbody2D monsterRB = spawnObj.AddComponent<Rigidbody2D>();
            monsterRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            monsterRB.gravityScale = 0f;
            SpriteRenderer render = spawnObj.AddComponent<SpriteRenderer>();

            render.sprite = DataContainer.monsterAtlas.GetSprite($"{MonsterManager.monstersByMap[SceneManager.GetActiveScene().name][spawnIdx - 1]}_1");

            MonsterInfo spawnInfo = MonsterManager.monsters[MonsterManager.monstersByMap[SceneManager.GetActiveScene().name][spawnIdx - 1]];
            monster.Health = spawnInfo.Health;
            monster.name = spawnInfo.Name;
            monster.damage = spawnInfo.Damage;
            monster.dropItem = spawnInfo.DropItem;

            monster.attackWaitSecond = 5f;

            Instantiate(monster, new Vector3(Mathf.Clamp(player.position.x + randX, minTilemap.x, maxTilemap.x),
                Mathf.Clamp(player.position.y + randY, minTilemap.y, maxTilemap.y)), new Quaternion(0, 0, 0, 0));
            Destroy(spawnObj);
        }
    }
}
