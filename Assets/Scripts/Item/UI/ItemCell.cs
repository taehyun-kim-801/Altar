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
    private WaitForSeconds clickTime = new WaitForSeconds(0.5f);
    private bool isOpenInfoUI = false;
    private bool isPointerExit = false;

    public void SetItemCell(string itemName, int itemNumber)
    { 
        if (itemImage == null)
            itemImage = GetComponent<Image>();
        if (itemNumberText == null)
            itemNumberText = GetComponentInChildren<Text>();

        if (itemNumber == 0)
        {
            itemImage.sprite = null;
            itemImage.color = new Color(0, 0, 0, 0);
            return;
        }

        this.itemName = itemName;
        if (itemNumber > 1)
        {
            itemNumberText.text = itemNumber.ToString();
            if (itemImage.color != Color.white) itemImage.color = Color.white;
        }
        else
            itemNumberText.text = null;

        itemImage.sprite = Item.itemDictionary[itemName].sprite;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        isPointerExit = false;
        StartCoroutine(OpenItemInfoUI());
    }

    public void OnPointerExit(PointerEventData data)
    {
        isPointerExit = true;
        if(isOpenInfoUI)
        {
            isOpenInfoUI = false;
            ItemInfoUI.CloseItemInfoUI();
        }
    }

    public IEnumerator OpenItemInfoUI()
    {
        yield return clickTime;
        if(isPointerExit)
            yield break;
            
        ItemInfoUI.OpenItemInfoUI(Item.itemDictionary[itemName], gameObject.transform.position);
        isOpenInfoUI = true;
        yield break;
    }
}
