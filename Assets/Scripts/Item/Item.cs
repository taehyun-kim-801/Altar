using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public string ItemName => itemName;
    public Sprite Sprite { get; set; }

    [SerializeField]
    protected string itemName;

    protected Item(string name)
    {
        itemName = name;
    }

    public abstract void UseItem(GameObject gameObject);
}