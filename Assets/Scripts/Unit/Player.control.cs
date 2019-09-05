using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Player
{
    private GameObject interactionObj;

    private float attackTime;
    public override void Interaction()
    {
        if (interactionObj.CompareTag("Altar"))
        {

        }
        else if(interactionObj.CompareTag("Portal"))
        {
            SceneManager.LoadScene(interactionObj.GetComponent<Portal>().nextScene);
        }
        else
        {
            if (inventory[invenIdx] != null)
            {
                if (ItemManager.Instance.GetItem(inventory[invenIdx]) is Food || ItemManager.Instance.GetItem(inventory[invenIdx]) is Sacrifice)
                    if (--invenQuantity[invenIdx] == 0)
                        inventory[invenIdx] = null;

                equippedItem.UseEquippedItem();
            }
        }
    }

    public void Eat(int satiety)
    {
        hunger += satiety;
    }

    public override void Hurt(int damage)
    {
        if (!isAttacked)
        {
            Health -= damage;
            if (Health <= 0)
            {
                StopAllCoroutines();
                Die();
            }

            canMove = false;
            StartCoroutine(Stiff());

            isAttacked = true;
            StartCoroutine(Blink());
            StartCoroutine(Invincible());

            Debug.Log("Player Health: " + health + " Hunger: " + hunger);
        }
        
    }

    public void PickUp()
    {
        Debug.Log("Pick Up");
    }
}
