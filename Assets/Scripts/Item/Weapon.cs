using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int attackStat;
    public float coolTime;

    public override void UseItem(GameObject gameObject)
    {
        gameObject.SendMessage("Attack");
    }

    public Weapon(string name,int attackStat,float coolTime):base(name)
    {
        this.attackStat = attackStat;
        this.coolTime = coolTime;
    }
}
