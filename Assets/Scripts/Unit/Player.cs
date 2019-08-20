using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Unit
{
    public float maxDistance;
    public float invincibleTime;

    private Rigidbody rigidbody;

    public GameObject gameManager;
    private ItemManager itemManager;

    public Text interactionText;

    void Start()
    {
        hunger = 100f;
        health = 10;
        isAttacked = false;

        interactionObj = null;

        rigidbody = GetComponent<Rigidbody>();

        inventory = new string[5];
        invenQuantity = new int[5];

        itemManager = gameManager.GetComponent<ItemManager>();
    }

    void FixedUpdate()
    {
        if (hunger > 0f)
            hunger -= Time.deltaTime / 6;
        else
            StartCoroutine(DecreaseHealth());

        Move(faceDirection);

        if (interactionObj != null)
        {
            Vector3 scale = transform.localScale;
            if (interactionObj.transform.position.x > transform.position.x)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;

            if (interactionObj.CompareTag("Altar"))
            {
                interactionText.text = "제단";
            }
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
        float minDistance = maxDistance * maxDistance;
        foreach (var collider in colliders) {
            if (collider.transform.position != transform.position)
            {
                float tempDistance = Vector3.SqrMagnitude(collider.transform.position - transform.position);
                if (tempDistance <= minDistance)
                {
                    minDistance = tempDistance;
                    interactionObj = collider.gameObject;
                }
            }
        }

        if(isAttacked)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 0.5f * Mathf.Cos((Time.time - attackTime) * Mathf.Rad2Deg * 2 * Mathf.PI) + 0.5f;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
