using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public int damage;
    public float attackWaitSecond = 5f;

    public string dropItem;
    private ItemManager manager;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;
        foundPlayer = false;
        manager = FindObjectOfType<ItemManager>();
        StartCoroutine(RandomDirection());
    }

    private bool foundPlayer;
    // Update is called once per frame
    void Update()
    {
        if (Player.Instance != null)
        {
            if (!foundPlayer && Vector3.SqrMagnitude(transform.position - Player.Instance.transform.position) <= 25.0f)
            {
                foundPlayer = true;
                StopCoroutine(RandomDirection());
                Interaction();
            }
            else if (foundPlayer)
            {
                faceDirection = (Player.Instance.transform.position - transform.position).normalized;
                if (Player.Instance.transform.position.x < transform.position.x)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = -Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
                else
                {
                    Vector3 scale = transform.localScale;
                    scale.x = Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
            }

            Move(faceDirection);
        }
    }

    public IEnumerator RandomDirection()
    {
        while(true)
        {
            float second = Random.Range(0.5f, 1.5f);
            float x = Random.Range(-1.0f, 1.0f);
            float y = Random.Range(-1.0f, 1.0f);

            faceDirection = new Vector3(x, y).normalized;
            yield return new WaitForSeconds(second);
        }
    }

    public override void Interaction()
    {
        StartCoroutine(Attack());
    }

    public override void Hurt(int damage)
    {
        health -= damage;
        Debug.Log("Monster health: " + health);

        if(health<=0)
        {
            Die();
        }
    }

    public IEnumerator Attack()
    {
        while(Player.Instance != null)
        {
            yield return new WaitUntil(() => Vector3.Distance(transform.position, Player.Instance.transform.position) <= 0.7f);
            Player.Instance.Hurt(damage);
            yield return new WaitForSeconds(attackWaitSecond);
        }
    }

    protected override void Die()
    {
        StopCoroutine(Attack());
        if(Random.Range(0f,1f)<=0.7f)
        {
            ItemManager.Instance.DropItem(dropItem, transform.position);
        }
        base.Die();
    }
}
