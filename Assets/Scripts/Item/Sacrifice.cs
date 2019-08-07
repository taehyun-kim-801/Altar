using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrifice : Item
{
    public readonly int satiety;
    public readonly int damage;

    public Sacrifice(string name, Sprite sprite, int satiety, int hurt) : base(name, sprite)
    {
        this.satiety = satiety;
        this.damage = hurt;
    }

    public override void UseItem(GameObject gameObject)
    {
        gameObject.SendMessage("Eat", satiety);
        gameObject.SendMessage("Hurt", damage);
    }
}