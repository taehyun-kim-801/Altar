using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public System.Action clickFunc;

    private Image itemImage;
    private Text itemNumberText;
    private string itemName;
    private float clickTime = 0.5f;
    private bool isOpenInfoUI = false;

    public void SetItemCell(string itemName, int itemNumber)
    {
        if (itemImage == null)
            itemImage = GetComponent<Image>();
        if (itemNumberText == null)
            itemNumberText = GetComponentInChildren<Text>();

        this.itemName = itemName;
        if(itemNumber > 1)
            itemNumberText.text = itemNumber.ToString();
        itemImage.sprite = Item.itemDictionary[itemName].sprite;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        StartCoroutine("OpenItemInfoUI");
        clickFunc();
    }

    public void OnPointerExit(PointerEventData data)
    {
        StopCoroutine("OpenItemInfoUI");
        if(isOpenInfoUI)
        {
            isOpenInfoUI = false;
            ItemInfoUI.CloseItemInfoUI();
        }
    }

    public IEnumerator OpenItemInfoUI()
    {
        yield return new WaitForSeconds(clickTime);
        ItemInfoUI.OpenItemInfoUI(Item.itemDictionary[itemName], transform);
        isOpenInfoUI = true;
        yield return null;
    }
}
