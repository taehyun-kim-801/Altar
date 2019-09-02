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

    private GameObject thrownObject;

    public RangedWeapon(string name, string thrownObjectName, List<Vector2> thrownObjectDirection, float delay) : base(name)
    {
        this.delay = delay;
        this.thrownObjectName = thrownObjectName;
        this.thrownObjectDirection = thrownObjectDirection;
    }

    public void LoadThrownObject()
    {

    }

    public override void Equip(EquippedItem equipedItem)
    {
        equipedItem.UseItem = Attack;
    }

    public void Attack()
    { 
        thrownObjectDirection.ForEach((direction) => { Object.Instantiate(thrownObject).GetComponent<ThrownObject>().StartCoroutine("MoveThrownObject"); });
    }
}
