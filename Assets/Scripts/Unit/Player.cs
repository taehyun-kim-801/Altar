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

    public GameObject hand;
    private float handDistance;

    void Start()
    {
        hunger = 100f;
        health = 10;
        isAttacked = false;

        interactionObj = null;

        rigidbody = GetComponent<Rigidbody>();

        inventory = new string[5];
        invenQuantity = new int[5];

        Debug.Log(inventory.Length);
        itemManager = gameManager.GetComponent<ItemManager>();

        handDistance = Vector3.Distance(hand.transform.position, new Vector3(0, -0.05f));
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
            else if(interactionObj.CompareTag("Portal"))
            {
                interactionText.text = "이동";
            }
            else
            {
                interactionText.text = "공격";
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

            interactionText.text = "상호작용";
        }

        interactionObj = null;

        var colliders = Physics.OverlapSphere(transform.position, maxDistance);
        float minDistance = maxDistance * maxDistance;
        foreach (var collider in colliders) {
            if (collider.transform.position != transform.position)
            {
                if(collider.CompareTag("Portal"))
                {
                    if (Vector3.SqrMagnitude(collider.transform.position - transform.position) <= 1.0f)
                    {
                        interactionObj = collider.gameObject;
                        break;
                    }
                    else continue;
                }
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

        if (faceDirection != Vector3.zero)
            hand.transform.position = transform.position - new Vector3(0,-0.05f) + faceDirection * handDistance;
    }
}
