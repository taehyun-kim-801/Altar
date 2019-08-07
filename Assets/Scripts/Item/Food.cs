using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    public readonly int satiety;

    public Food(string name, Sprite sprite, int satiety) : base(name, sprite)
    {
        this.satiety = satiety;
    }

    public override void UseItem(GameObject gameObject)
    {
        gameObject.SendMessage("Eat", satiety);
    }
}
