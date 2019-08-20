using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    private Text nameText;
    private Text infoText;

    public void OpenItemInfo(Item item)
    {
        nameText.text = "이름 : " + item.Name;
        if(item is Food)
        {
            infoText.text = "포만감 : " + (item as Food).Satiety;
        }
        else if(item is Sacrifice)
        {
            infoText.text = "포만감 : " + (item as Sacrifice).Satiety
            + "\n 피해량" + (item as Sacrifice).Damage;
        }
    }
}
