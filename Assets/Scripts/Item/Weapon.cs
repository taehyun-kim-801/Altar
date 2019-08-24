using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int Damage => damage;
    public float Delay => delay;
    public string WeaponTypeName => weaponTypeName;

    [SerializeField]
    private int damage;
    [SerializeField]
    private float delay;
    [SerializeField]
    private string weaponTypeName;

    private WeaponType weaponType;
    private float time;

    public Weapon(string name, int damage, float delay) : base(name)
    {
        this.delay = delay;
        this.damage = damage;
    }

    public override void UseItem(GameObject gameObject)
    {
        weaponType.Attack(damage);
    }
}
