using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food : Item
{
    [SerializeField]
    private int satiety;

    public Food(string name, int satiety) : base(name)
    {
        this.satiety = satiety;
    }

    public override void Equip(EquippedItem equipedItem) => equipedItem.UseItem = Eat;

    public override string GetInfo() => $"포만감 : {satiety}";

    private void Eat() => Player.Instance.Eat(satiety);
}
