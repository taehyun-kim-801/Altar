using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food : Item
{
    public int Satiety => satiety;

    [SerializeField]
    private int satiety;

    public Food(string name, int satiety) : base(name)
    {
        this.satiety = satiety;
    }

    public override void UseItem(GameObject gameObject)
    {
        gameObject.SendMessage("Eat", satiety);
    }
}
