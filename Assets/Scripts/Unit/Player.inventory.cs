using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private string[] inventory;
    private int[] invenQuantity;
    public Item curItem;
    private int invenIdx;

    public Item GetCurrentItem()
    {
        return curItem;
    }
    public void SelectItem(int index)
    {
        if (inventory[index] != null)
        {
            curItem = itemManager.GetItem(inventory[index]);

            if (!interactionObj.CompareTag("Altar"))
            {
                if (itemManager.GetItem(inventory[invenIdx]) is Food || itemManager.GetItem(inventory[invenIdx]) is Sacrifice)
                {
                    interactionText.text = "먹기";
                }
                else if (itemManager.GetItem(inventory[invenIdx]) is Weapon)
                {
                    interactionText.text = "공격";
                }
            }
        }
    }
}
