using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                interactionObj.SendMessage("OpenAltarUI");
            }
            else if (interactionObj.CompareTag("Portal"))
            {
                MapManager mapManager = GameManager.Instance.GetComponent<MapManager>();
                var curScene = mapManager.sceneName;
                GameManager.Instance.GetComponent<MapManager>().nextScene = interactionObj.GetComponent<Portal>().nextScene;
                var portals = mapManager.portalTransformList;
                var changePos = from p in mapManager.portalTransformList where curScene == p.GetComponent<Portal>().nextScene select p;
                StartCoroutine(GameManager.Instance.GetComponent<MapManager>().ChangeScene(() => transform.position = (from p in mapManager.portalTransformList where curScene == p.GetComponent<Portal>().nextScene select p).First().transform.position));
            }
            else
            {
                if (inventory[invenIdx] != null)
                {
                    if (equippedItem.state == EquippedItem.State.None)
                    {
                        if (Item.itemDictionary[inventory[invenIdx]] is Food || Item.itemDictionary[inventory[invenIdx]] is Sacrifice)
                            if (--invenQuantity[invenIdx] == 0)
                            {
                                inventory[invenIdx] = null;
                                itemCells[invenIdx].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                                equippedItem.Equip();
                            }

                        equippedItem.UseEquippedItem();
                        itemCells[invenIdx].SetItemCell(inventory[invenIdx], invenQuantity[invenIdx]);
                    }
                }
            }
        }
        else
        {
            if(inventory[invenIdx]!=null && equippedItem.state == EquippedItem.State.None)
            {
                if (Item.itemDictionary[inventory[invenIdx]] is Food || Item.itemDictionary[inventory[invenIdx]] is Sacrifice)
                    if (--invenQuantity[invenIdx] == 0)
                    { 
                        inventory[invenIdx] = null;
                        itemCells[invenIdx].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                        equippedItem.Equip();
                    }

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
                return;
            }

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
            bool change = true;
            if (item.item is Food || item.item is Sacrifice)
            {
                for(int i=0;i<inventory.Length;i++)
                {
                    if(inventory[i]==item.item.name)
                    {
                        invenQuantity[i] += item.count;
                        change = false;
                        itemCells[i].SetItemCell(item.item.name, invenQuantity[i]);
                        break;
                    }
                }
            }

            if(change)
            {
                if(inventory[invenIdx]!=null)
                {
                    Item.DropItem(inventory[invenIdx], invenQuantity[invenIdx], transform.position);
                }
                else
                {
                    itemCells[invenIdx].GetComponent<Image>().color = Color.white;
                }

                inventory[invenIdx] = item.item.name;
                invenQuantity[invenIdx] = item.count;
                itemCells[invenIdx].SetItemCell(item.item.name, item.count);

                equippedItem.Equip(Item.itemDictionary[inventory[invenIdx]]);
            }

            Destroy(droppedItem);
            droppedItem = null;
        }
    }
}
