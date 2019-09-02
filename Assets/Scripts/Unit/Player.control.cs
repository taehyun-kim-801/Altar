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
                {
                    invenQuantity[invenIdx]--;

                }
                ItemManager.Instance.GetItem(inventory[invenIdx]).UseItem(gameObject);
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

            if (health <= 0)
            {
                Die();
                return;
            }

            canMove = false;
            StartCoroutine(Stiff());

            isAttacked = true;
            StartCoroutine(Blink());
            StartCoroutine(Invincible());

            Debug.Log("Player Health: " + health + " Hunger: " + hunger);
        }
    }

    public void Attack()
    {
        if (interactionObj != null && Vector3.SqrMagnitude(interactionObj.transform.position - transform.position) <= 1.0f && Time.time - attackTime >= (ItemManager.Instance.GetItem(inventory[invenIdx]) as Weapon).coolTime)
        {
            attackTime = Time.time;
            interactionObj.SendMessage("Hurt", gameObject);
        }
    }

    public void PickUp()
    {
        Debug.Log("Pick Up");
    }
}
