using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangedWeapon : Item
{
    public string ThrownObjectName => thrownObjectName;

    [SerializeField]
    private string thrownObjectName;
    [SerializeField]
    private List<Vector2> thrownObjectDirection;

    private Transform equippedItemTransform;

    public RangedWeapon(string name, string thrownObjectName, List<Vector2> thrownObjectDirection, float delay) : base(name)
    {
        this.delay = delay;
        this.thrownObjectName = thrownObjectName;
        this.thrownObjectDirection = thrownObjectDirection;
    }

    public override void Equip(EquippedItem equipedItem)
    {
        equipedItem.UseItem = Attack;
        equippedItemTransform = equipedItem.gameObject.transform;
    }

    public void Attack() => thrownObjectDirection.ForEach((direction) => { ProjectileManager.Instance.ActivateProjectile(thrownObjectName, equippedItemTransform.position, direction); });
}
