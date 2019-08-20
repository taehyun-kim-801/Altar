using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private GameObject interactionObj;

    private float attackTime;
    public override void Interaction()
    {
        if (interactionObj.CompareTag("Altar"))
        {

        }
        else
        {
            if (inventory[invenIdx] != null)
            {
                if (itemManager.GetItem(inventory[invenIdx]) is Food || itemManager.GetItem(inventory[invenIdx]) is Sacrifice)
                {
                    invenQuantity[invenIdx]--;

                }
                itemManager.GetItem(inventory[invenIdx]).UseItem(gameObject);
            }
        }
    }

    public void Eat(int satiety)
    {
        hunger += satiety;
    }

    public void Hurt(int damage)
    {
        health -= damage;
    }

    public void Attack()
    {
        if (interactionObj != null && Vector3.SqrMagnitude(interactionObj.transform.position - transform.position) <= 1.0f && Time.time - attackTime >= (itemManager.GetItem(inventory[invenIdx]) as Weapon).coolTime)
        {
            attackTime = Time.time;
            interactionObj.SendMessage("GetAttack", gameObject);
        }
    }

    public void PickUp()
    {
        Debug.Log("Pick Up");
    }
}
