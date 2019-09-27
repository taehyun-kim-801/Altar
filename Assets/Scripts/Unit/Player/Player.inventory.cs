using System.Collections;
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

    public void SetInventory(string[] inventory)
    {
        this.inventory = inventory;
    }

    public void SetInvenQuantity(int[] invenQuantity)
    {
        this.invenQuantity = invenQuantity;
    }

    public int CheckQuantity(string item)
    {
        int result = -1;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
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
                if(invenQuantity[i]==0)
                {
                    inventory[i] = null;
                    if(i == invenIdx)
                    {
                        equippedItem.Equip();
                    }
                }

                itemCells[i].SetItemCell(inventory[i], invenQuantity[i]);
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
        invenIdx = index;
        if (inventory[index] != null)
        {
            equippedItem.Equip(Item.itemDictionary[inventory[invenIdx]]);
        }
        else equippedItem.Equip();
    }


}
