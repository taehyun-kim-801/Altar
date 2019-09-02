using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food : Item
{
    public int Satiety => satiety;

    [SerializeField]
    private int satiety;
    private Player player;

    public Food(string name, int satiety) : base(name)
    {
        this.satiety = satiety;
    }

    public override void Equip(EquippedItem equipedItem)
    {
        equipedItem.UseItem = Eat;
        player = equipedItem.PlayerInstance;
    }

    private void Eat() => player.Eat(satiety);
}
