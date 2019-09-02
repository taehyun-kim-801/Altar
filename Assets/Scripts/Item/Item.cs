using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string ItemName => itemName;
    public Sprite ItemSprite { get; set; }
    public WaitForSeconds itemDelaySeconds => new WaitForSeconds(delay);

    [SerializeField]
    private string itemName;
    [SerializeField]
    protected float delay = 0.5f;

    protected Item(string name)
    {
        itemName = name;
    }

    public virtual void Equip(EquippedItem equipedItem) { }
}