using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private float _hunger;
    public float hunger
    {
        get { return _hunger; }
        set
        {
            _hunger = Mathf.Clamp(value, 0f, 100f);
        }
    }

    public List<Item> inventory;
    public float maxDistance;

    private GameObject interactionObj;

    private bool canMove;
    private bool isAttacked;
    public float invincibleTime;

    private Rigidbody rigidbody;

    void Start()
    {
        hunger = 100f;
        health = 100;
        isAttacked = false;
        canMove = true;

        interactionObj = null;

        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (hunger > 0f)
            hunger -= Time.deltaTime / 6;
        else
            health -= health / 60 * Time.deltaTime;

        Move(faceDirection);

        if (interactionObj!=null)
        {
            Vector3 scale = transform.localScale;
            if (interactionObj.transform.position.x > transform.position.x)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;

            Debug.Log(interactionObj.tag);
        }
        else
        {
            Vector3 scale = transform.localScale;
            if (faceDirection.x >= 0)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;
        }

        interactionObj = null;

        var colliders = Physics.OverlapSphere(transform.position, maxDistance);
        float minDistance = maxDistance;
        foreach(var collider in colliders) {
            if (collider.transform.position != transform.position)
            {
                float tempDistance = Vector3.Distance(collider.transform.position, transform.position);
                if (tempDistance <= minDistance)
                {
                    minDistance = tempDistance;
                    interactionObj = collider.gameObject;
                }
            }
        }
    }

    public override void Interaction()
    {
        Debug.Log("Interaction");
        if (interactionObj != null)
        {
            if (interactionObj.CompareTag("Monster"))
            {
                Monster monster = interactionObj.GetComponent<Monster>();
                monster.GetAttack(gameObject);
            }
        }
        return;
    }

    public override void GetAttack(GameObject enemy)
    {
        if (rigidbody.isKinematic)
        {
            Monster monster = enemy.GetComponent<Monster>();

            health -= monster.attackStat;

            isAttacked = true;
            StartCoroutine(Invincible(monster));

            if (health <= 0)
            {
                Die();
                return;
            }

            Debug.Log("Player Health: " + health + " Hunger: " + hunger);
        }
    }

    public IEnumerator Invincible(Monster monster)
    {
        rigidbody.isKinematic = false;

        yield return new WaitForSeconds(invincibleTime);

        rigidbody.isKinematic = true;
    }

    public float attackStat;

    public void PickUp()
    {
        Debug.Log("Pick Up");
    }

    protected override void Die()
    {
        Time.timeScale = 0f;
        base.Die();
    }
}
