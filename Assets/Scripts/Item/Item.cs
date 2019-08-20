using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    [SerializeField]
    protected string name;
    public string Name => name;

    public readonly Sprite sprite;

    protected Item(string name)
    {
        this.name = name;
        sprite = Resources.Load<Sprite>(name);
    }
    public abstract void UseItem(GameObject gameObject);
}