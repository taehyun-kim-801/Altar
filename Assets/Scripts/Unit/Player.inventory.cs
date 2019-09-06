using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private string[] inventory;
    private int[] invenQuantity;
    private EquippedItem equippedItem;
    private int invenIdx;

    public string[] Inventory { get; private set; }
    public int[] InvenQuantity { get; private set; }

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
            equippedItem.Equip(ItemManager.Instance.GetItem(inventory[index]));

            if (!interactionObj.CompareTag("Altar"))
            {
                if (ItemManager.Instance.GetItem(inventory[invenIdx]) is Food || ItemManager.Instance.GetItem(inventory[invenIdx]) is Sacrifice)
                {
                    interactionText.text = "먹기";
                }
                else if (ItemManager.Instance.GetItem(inventory[invenIdx]) is MeleeWeapon || ItemManager.Instance.GetItem(inventory[invenIdx]) is RangedWeapon)
                {
                    interactionText.text = "공격";
                }
            }
        }
    }


}
