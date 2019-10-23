using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sacrifice : Item
{
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

    public override string GetInfo() => $"포만감 : {satiety}\n피해량 : {damage}";

    private void Eat()
    {
        Player.Instance.Eat(satiety);
        Player.Instance.Hurt(damage);
    }
}