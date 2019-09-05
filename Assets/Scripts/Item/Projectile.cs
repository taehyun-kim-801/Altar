using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int index { get; private set; }

    private float speed;
    private float damage;
    private float distance;
    private Vector3 direction;
    private Vector3 startPoint;
    private WaitForSeconds moveSeconds = new WaitForSeconds(0.1f);

    public void Set(int index, float speed, float damage, float distance ,Vector3 startPoint, Vector3 direction, Sprite sprite)
    {
        transform.Translate(startPoint);

        this.index = index;
        this.speed = speed;
        this.damage = damage;
        this.distance = distance;
        this.startPoint = startPoint;
        this.direction = direction;

        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        StartCoroutine("Move");
    }

    public IEnumerator Move()
    {
        float moveTime = distance / speed * 10;
        for(int i = 0; i < moveTime; i++)
        {
            transform.Translate(direction * speed * 0.1f);
            yield return moveSeconds;
        }
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();

            // monster.GetAttack(damage);
            gameObject.SetActive(false);
            ProjectileManager.Instance.DeactivateProjectile(index);
        }
    }
}
