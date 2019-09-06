using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text infoText;

    public void OpenItemInfoUI(Item item)
    {
        transform.SetAsLastSibling();

        nameText.text = $"이름 : {item.ItemName}";

        switch (item)
        {
            case Food food:
                infoText.text = $"포만감 : {food.Satiety}";
                break;
            case Sacrifice sacrifice:
                infoText.text = $"포만감 : {sacrifice.Satiety}\n 피해량 : {sacrifice.Damage}";
                break;
        }
    }
}
