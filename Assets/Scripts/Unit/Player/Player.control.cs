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
                if (Item.itemDictionary[inventory[invenIdx]] is Food || Item.itemDictionary[inventory[invenIdx]] is Sacrifice)
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
        if(droppedItem!=null)
        {
            if (droppedItem.GetComponent<DroppedItem>().item is MeleeWeapon || droppedItem.GetComponent<DroppedItem>().item is RangedWeapon) { }
            if (inventory[invenIdx]!=null)
            {
                DropItem();
            }

            
            Destroy(droppedItem);
            droppedItem = null;
        }
    }

    public void DropItem()
    {
        GameObject dropItem = new GameObject(inventory[invenIdx]);
        SpriteRenderer renderer = dropItem.AddComponent<SpriteRenderer>();
        renderer.sprite = DataContainer.itemAtlas.GetSprite(inventory[invenIdx]);

        DroppedItem item = dropItem.AddComponent<DroppedItem>();
        item.DropItem(Item.itemDictionary[inventory[invenIdx]], invenQuantity[invenIdx], transform.position);

        inventory[invenIdx] = null;
        invenQuantity[invenIdx] = 0;
    }
}
