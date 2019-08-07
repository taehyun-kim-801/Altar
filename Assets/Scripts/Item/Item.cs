using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public readonly string name;
    public readonly Sprite sprite;

    protected Item(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }

    public abstract void UseItem(GameObject gameObject);
}