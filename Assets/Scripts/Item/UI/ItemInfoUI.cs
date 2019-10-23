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

    public static void OpenItemInfoUI(Item item, Transform parentTransform)
    {
        instance.backgroundImage.gameObject.SetActive(true);
        instance.OpenItemInfo(item, parentTransform);
    }

    public static void CloseItemInfoUI() => instance.backgroundImage.gameObject.SetActive(false);

    public void OpenItemInfo(Item item, Transform parentTransform)
    {
        backgroundImage.transform.position = parentTransform.position;

        nameText.text = $"이름 : {item.name}";
        infoText.text = item.GetInfo();
    }
}
