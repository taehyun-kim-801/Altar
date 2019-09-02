using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float distance;
    [SerializeField]
    private string thrownObjectName;

    private Sprite sprite;
    private Vector2 direction;
    private Vector2 startPoint;

    public ThrownObject(float speed, float damage, float distance, string name)
    {
        this.speed = speed;
        this.damage = damage;
        this.distance = distance;
        thrownObjectName = name;
    }

    void Start()
    {
        startPoint = transform.position;
    }
    
    public IEnumerator MoveThrownObject(Vector2 direction)
    {
        while (true)
        {
            transform.Translate(direction * 1 * Time.deltaTime);
            CheckRange();
            yield return null;
        }
    }

    private void CheckRange()
    {
        if (Vector2.Distance(startPoint, transform.position) > distance)
        {
            StopAllCoroutines();
            Destroy(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();

            // monster.GetAttack(damage);
            Destroy(this);
        }
    }
}
