using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public int attackStat;
    public float attackWaitSecond;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = target.GetComponent<Player>();
        foundPlayer = false;
        StartCoroutine(RandomDirection());
    }

    private bool foundPlayer;
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (!foundPlayer && Vector3.SqrMagnitude(transform.position - target.transform.position) <= 25.0f)
            {
                foundPlayer = true;
                StopCoroutine(RandomDirection());
                Interaction();
            }
            else if (foundPlayer)
            {
                faceDirection = (target.transform.position - transform.position).normalized;
                if (target.transform.position.x < transform.position.x)
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
    
    public GameObject target;
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

    public override void GetAttack(GameObject enemy)
    {
        Player player = enemy.GetComponent<Player>();
        health -= (player.curItem as Weapon).Damage;
        Debug.Log("Monster health: " + health);

        if(health<=0)
        {
            Die();
        }
    }

    public IEnumerator Attack()
    {
        while(player != null)
        {
            yield return new WaitUntil(() => Vector3.Distance(transform.position, target.transform.position) <= 1.0f);
            player.GetAttack(this.gameObject);
            yield return new WaitForSeconds(attackWaitSecond);
        }
    }

    protected override void Die()
    {
        StopCoroutine(Attack());
        if(Random.Range(0f,1f)<=0.7f)
        {
            Debug.Log("Item Drop");
        }
        base.Die();
    }
}
