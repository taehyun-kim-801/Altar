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

    public override void Equip(EquippedItem equipedItem) => equipedItem.UseItem = Eat;

    private void Eat()
    {
        Player.Instance.Eat(satiety);
        Player.Instance.Hurt(damage);
    }
}