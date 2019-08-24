using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image itemImage;
    private Text itemNumberText;
    private string itemName;

    public void SetItemCell(string itemName, int itemNumber)
    {
        if (itemImage == null)
            itemImage = GetComponent<Image>();
        if (itemNumberText == null)
            itemNumberText = GetComponentInChildren<Text>();

        this.itemName = itemName;
        if(itemNumber > 1)
            itemNumberText.text = itemNumber.ToString();
        itemImage.sprite = ItemManager.Instance.GetItem(itemName).Sprite;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        ItemManager.Instance.OpenItemInfo(itemName, transform);
    }

    public void OnPointerExit(PointerEventData data)
    {
        ItemManager.Instance.CloseItemInfo();
    }
}
