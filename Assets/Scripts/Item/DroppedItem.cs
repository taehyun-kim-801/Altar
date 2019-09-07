using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    private Item item;
    private int count;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void DropItem(Item item, int count, Vector3 position)
    {
        this.item = item;
        this.count = count;
        spriteRenderer.sprite = item.sprite;
        gameObject.transform.position = position;
    }
}
