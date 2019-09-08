using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeleeWeapon : Item
{
    public int Damage => damage;

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
        if (equipedItem.GetComponent<PolygonCollider2D>() != null)
            Object.DestroyImmediate(equipedItem.GetComponent<PolygonCollider2D>());
        equipedItem.gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;

        equipedItem.UseItem = null;
        equipedItem.triggerFunc = Attack;
    }

    public void Attack(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Monster"))
        { 
            collider2D.gameObject.GetComponent<Monster>().Hurt(damage);
        }
    }
}
