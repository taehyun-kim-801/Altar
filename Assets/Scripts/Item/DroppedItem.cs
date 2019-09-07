using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public Item item { get; private set; }
    public int count { get; private set; }
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
