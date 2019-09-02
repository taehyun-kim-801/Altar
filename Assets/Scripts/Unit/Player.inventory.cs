using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private string[] inventory;
    private int[] invenQuantity;
    private GameObject equipItem;
    private int invenIdx;

    public GameObject GetCurrentItem()
    {
        return equipItem;
    }
    public void SelectItem(int index)
    {
        if (inventory[index] != null)
        {
            //equipItem = ItemManager.Instance.GetItem(inventory[index]);

            if (!interactionObj.CompareTag("Altar"))
            {
                if (ItemManager.Instance.GetItem(inventory[invenIdx]) is Food || ItemManager.Instance.GetItem(inventory[invenIdx]) is Sacrifice)
                {
                    interactionText.text = "먹기";
                }
                else if (ItemManager.Instance.GetItem(inventory[invenIdx]) is Weapon)
                {
                    interactionText.text = "공격";
                }
            }
        }
    }
}
