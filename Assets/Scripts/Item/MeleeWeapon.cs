using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeleeWeapon : Item
{
    [SerializeField]
    private int damage;
    private EquippedItem equippedItem;

    public MeleeWeapon(string name, int damage, float delay) : base(name)
    {
        this.delay = delay;
        this.damage = damage;
    }

    public override void Equip(EquippedItem equipedItem)
    {
        equipedItem.UseItem = null;
        equipedItem.triggerFunc = Attack;
    }

    public override string GetInfo() => $"공격력 : {damage}\n속도 : {delay}";

    public void Attack(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Monster"))
        {
            collider2D.gameObject.GetComponent<Monster>().Hurt(damage);
        }
    }
}
