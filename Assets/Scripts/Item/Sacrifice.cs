using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sacrifice : Item
{
    public int Satiety => satiety;
    public int Damage => damage;

    [SerializeField]
    private int satiety;
    [SerializeField]
    private int damage;

    public Sacrifice(string name, int satiety, int damage) : base(name)
    {
        this.satiety = satiety;
        this.damage = damage;
    }

    public override void UseItem(GameObject gameObject)
    {
        gameObject.SendMessage("Eat", satiety);
        gameObject.SendMessage("GetAttack", damage);
    }

    public int GetSatiety() { return satiety; }
}