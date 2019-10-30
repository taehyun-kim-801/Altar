using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    private static ItemInfoUI instance;

    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text infoText;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void OpenItemInfoUI(Item item, Vector3 parentPosition)
    {
        instance.OpenItemInfo(item, parentPosition);
    }

    public static void CloseItemInfoUI() => instance.backgroundImage.gameObject.SetActive(false);

    private void OpenItemInfo(Item item, Vector3 parentPosition)
    {
        backgroundImage.gameObject.SetActive(true);
        backgroundImage.transform.position = parentPosition;

        nameText.text = $"이름 : {item.name}";
        infoText.text = item.GetInfo();
    }
}
