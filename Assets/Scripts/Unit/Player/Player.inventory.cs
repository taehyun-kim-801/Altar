﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private string[] inventory;
    private int[] invenQuantity;
    private EquippedItem equippedItem;
    private int invenIdx;

    private GameObject droppedItem;

    public string[] Inventory => inventory;
    public int[] InvenQuantity => invenQuantity;
    public EquippedItem EquippedItem => equippedItem;
    public int InvenIdx => invenIdx;

    private ItemCell[] itemCells;

    public int CheckQuantity(string item)
    {
        int result = -1;
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i]==item)
            {
                result = invenQuantity[i];
                break;
            }
        }

        return result;
    }

    public void UseInventory(string itemName, int itemCount)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == itemName)
            {
                invenQuantity[i] -= itemCount;
                break;
            }
        }
    }

    public GameObject GetCurrentItem()
    {
        return equippedItem.gameObject;
    }

    public void SelectItem(int index)
    {
        if (inventory[index] != null)
        {
            equippedItem.Equip(Item.itemDictionary[inventory[invenIdx]]);

            if (!interactionObj.CompareTag("Altar"))
            {
                if (Item.itemDictionary[inventory[invenIdx]] is Food || Item.itemDictionary[inventory[invenIdx]] is Sacrifice)
                {
                    interactionText.text = "먹기";
                }
                else if (Item.itemDictionary[inventory[invenIdx]] is MeleeWeapon || Item.itemDictionary[inventory[invenIdx]] is RangedWeapon)
                {
                    interactionText.text = "공격";
                }
            }
        }
    }


}