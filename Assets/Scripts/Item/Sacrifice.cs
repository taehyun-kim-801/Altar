using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sacrifice : Item
{
    [SerializeField]
    private int satiety;
    public int Satiety => satiety;

    [SerializeField]
    private int damage;
    public int Damage => damage;

    public Sacrifice(string name, int satiety, int damage) : base(name)
    {
        this.satiety = satiety;
        this.damage = damage;
    }

    public override void UseItem(GameObject gameObject)
    {
        gameObject.SendMessage("Eat", satiety);
        gameObject.SendMessage("Hurt", damage);
    }
    public int GetSatiety() { return satiety; }
}