using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int index;

    private float speed;
    private float damage;
    private float distance;
    private bool hit;
    private Vector3 startPoint;

    public void Set(int index, float speed, float damage, float distance, Vector3 startPoint, Vector3 direction, Sprite sprite)
    {
        transform.position = startPoint;

        this.index = index;
        this.speed = speed;
        this.damage = damage;
        this.startPoint = startPoint;
        this.distance = distance;
        hit = false;

        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        if (gameObject.GetComponent<PolygonCollider2D>() != null)
            DestroyImmediate(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 45f);

        StartCoroutine("Move");
    }

    public IEnumerator Move()
    {
        while (!hit && Vector3.SqrMagnitude(startPoint - gameObject.transform.position) < distance * distance)
        {
            transform.Translate(new Vector2(1, 1) * speed * Time.deltaTime);
            yield return null;
        }
        ProjectileManager.Instance.DeactivateProjectile(index);
        gameObject.SetActive(false);
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<Monster>().Hurt((int)damage);
            hit = true;
        }
    }
}
