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
    }

    public void DropItem(Item item, int count, Vector3 position)
    {
        this.item = item;
        this.count = count;
        gameObject.AddComponent<SpriteRenderer>().sprite = item.sprite;
        gameObject.transform.localScale *= 5;
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        gameObject.tag = "DroppedItem";
        gameObject.transform.position = position;
    }
}
