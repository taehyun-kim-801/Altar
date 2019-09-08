using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class Player
{
    private GameObject interactionObj;

    private float attackTime;
    public override void Interaction()
    {
        if (interactionObj != null)
        {
            if (interactionObj.CompareTag("Altar"))
            {

            }
            else if (interactionObj.CompareTag("Portal"))
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
                    itemCells[invenIdx].SetItemCell(inventory[invenIdx], invenQuantity[invenIdx]);
                }
            }
        }
        else
        {
            if(inventory[invenIdx]!=null)
            {
                if (Item.itemDictionary[inventory[invenIdx]] is Food || Item.itemDictionary[inventory[invenIdx]] is Sacrifice)
                    if (--invenQuantity[invenIdx] == 0)
                        inventory[invenIdx] = null;

                equippedItem.UseEquippedItem();
                itemCells[invenIdx].SetItemCell(inventory[invenIdx], invenQuantity[invenIdx]);
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
        }
        
    }

    public void PickUp()
    {
        if(droppedItem!=null)
        {
            DroppedItem item = droppedItem.GetComponent<DroppedItem>();
            bool change = false;
            if (item.item is Food || item.item is Sacrifice)
            {
                for(int i=0;i<inventory.Length;i++)
                {
                    if(inventory[i]==item.item.name)
                    {
                        invenQuantity[i] += item.count;
                        change = true;
                        itemCells[i].SetItemCell(item.item.name, item.count);
                        break;
                    }
                }
            }

            if(change)
            {
                if(inventory[invenIdx]!=null)
                {
                    DropItem();
                }
                else
                {
                    itemCells[invenIdx].GetComponent<Image>().color = Color.white;
                }

                inventory[invenIdx] = item.item.name;
                invenQuantity[invenIdx] = item.count;
                itemCells[invenIdx].SetItemCell(item.item.name, item.count);
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
