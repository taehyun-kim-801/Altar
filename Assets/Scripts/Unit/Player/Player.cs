using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Unit
{
    public static Player Instance { get; private set; }
    public float maxDistance;
    public float invincibleTime;

    public GameObject gameManager;

    public Text interactionText;

    public Transform hand;
    private float handDistance;

    public GameObject inventoryUI;
    void Start()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        hunger = 100f;
        health = 10;
        isAttacked = false;

        interactionObj = null;

        inventory = new string[5];

        invenQuantity = new int[5];

        handDistance = Vector3.Distance(hand.transform.position, new Vector3(0, -0.05f));

        canMove = true;

        blinkColor = new Color[2] { new Color(0, 0, 0, 0), GetComponent<SpriteRenderer>().color };

        pinnedRecipes = new List<string>();

        equippedItem = hand.GetComponent<EquippedItem>();
        equippedItem.Bind(Instance);

        itemCells = inventoryUI.GetComponentsInChildren<ItemCell>();

        inventory[0] = "Knife";
        invenQuantity[0] = 1;

        itemCells[0].SetItemCell(inventory[0], invenQuantity[0]);
        itemCells[0].GetComponent<Image>().color = Color.white;

        SelectItem(0);
    }

    void Update()
    {
        if (hunger > 0f)
            hunger -= Time.deltaTime / 6;
        else
            StartCoroutine(DecreaseHealth());

        if(canMove)
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

        var colliders = Physics2D.OverlapCircleAll(transform.position, maxDistance);

        float minDistance = maxDistance * maxDistance;
        foreach (var collider in colliders) {
            if (!collider.CompareTag("Player"))
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

                if(tempDistance<=1.0f && collider.CompareTag("DroppedItem"))
                {
                    droppedItem = collider.gameObject;
                }
                else if (tempDistance <= minDistance)
                {
                    minDistance = tempDistance;
                    interactionObj = collider.gameObject;
                }
            }
        }

        if (faceDirection != Vector3.zero)
        {
            hand.position = transform.position - new Vector3(0, -0.05f) + faceDirection * handDistance;

            hand.GetComponent<SpriteRenderer>().flipX = faceDirection.x >= 0 ? false : true;
            hand.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg - 45f);
        }
    }
}
