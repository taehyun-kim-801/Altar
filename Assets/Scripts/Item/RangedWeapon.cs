using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangedWeapon : Item
{
    public string ProjectileName => projectileName;

    [SerializeField]
    private string projectileName;
    [SerializeField]
    private List<Vector2> projectileDirection;

    private Transform equippedItemTransform;

    public RangedWeapon(string name, string projectileName, List<Vector2> projectileDirection, float delay) : base(name)
    {
        this.delay = delay;
        this.projectileName = projectileName;
        this.projectileDirection = projectileDirection;
    }

    public override void Equip(EquippedItem equipedItem)
    {
        equipedItem.UseItem = Attack;
        equippedItemTransform = equipedItem.gameObject.transform;
    }

    public void Attack() => projectileDirection.ForEach((direction) => { 
        ProjectileManager.Instance.ActivateProjectile(projectileName, equippedItemTransform, direction);
    });
}
