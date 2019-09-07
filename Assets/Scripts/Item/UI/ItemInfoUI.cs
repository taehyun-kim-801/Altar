using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    private static ItemInfoUI instance;

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
        instance.gameObject.SetActive(true);
        instance.OpenItemInfo(item, parentTransform);
    }

    public static void CloseItemInfoUI() => instance.gameObject.SetActive(false);

    public void OpenItemInfo(Item item, Transform parentTransform)
    {
        transform.SetAsLastSibling();
        gameObject.transform.position = parentTransform.position;

        nameText.text = $"이름 : {item.name}";

        switch (item)
        {
            case Food food:
                infoText.text = $"포만감 : {food.Satiety}";
                break;
            case Sacrifice sacrifice:
                infoText.text = $"포만감 : {sacrifice.Satiety}\n 피해량 : {sacrifice.Damage}";
                break;
            case MeleeWeapon meleeWeapon:
                infoText.text = $"공격력 : {meleeWeapon.Damage}\n 속도 : {meleeWeapon.Delay}";
                break;
            case RangedWeapon rangedWeapon:
                infoText.text = $"공격력 : {ProjectileManager.Instance.GetProjectileDamage(rangedWeapon.ProjectileName)}\n 속도 : {rangedWeapon.Delay}";
                break;
        }
    }
}
