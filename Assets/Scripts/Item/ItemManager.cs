using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [SerializeField]
    private ItemInfoUI itemInfoUI;
    private Dictionary<string, Item> itemDictionary;
    private Sprite droppedItem;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        itemDictionary = new Dictionary<string, Item>();
    }

    private void Start()
    {
        LoadItemJson();
    }

    public void DropItem(string itemName, Vector3 position)
    {
        droppedItem = itemDictionary[itemName].ItemSprite;
        Instantiate(droppedItem, position, Quaternion.Euler(0, 0, 0)).name = itemDictionary[itemName].ItemName;
    }

    public Item GetItem(string itemName) => itemDictionary[itemName];

    public void OpenItemInfo(string itemName, Transform transform)
    {
        itemInfoUI.gameObject.SetActive(true);
        itemInfoUI.OpenItemInfoUI(itemDictionary[itemName]);
        itemInfoUI.transform.position = transform.position;
    }

    public void CloseItemInfo()
    {
        itemInfoUI.gameObject.SetActive(false);
    }

    private void LoadItemSprite()
    {
        List<Sprite> foodSprite = new List<Sprite>(Resources.LoadAll<Sprite>("Food"));
        foodSprite.ForEach((sprite) =>
        {
            if (itemDictionary.ContainsKey(sprite.name))
            {
                itemDictionary[sprite.name].ItemSprite = sprite;
            }
        });
    }

    private void LoadItemJson()
    {
        JsonManager.LoadJson<Food>().ForEach((food) => { itemDictionary.Add(food.ItemName, food); });

        JsonManager.LoadJson<Sacrifice>().ForEach((sacrifice) => { itemDictionary.Add(sacrifice.ItemName, sacrifice); });

        JsonManager.LoadJson<MeleeWeapon>().ForEach((meleeWeapon) => { itemDictionary.Add(meleeWeapon.ItemName, meleeWeapon); });

        JsonManager.LoadJson<RangedWeapon>().ForEach((rangedWeapon) => { itemDictionary.Add(rangedWeapon.ItemName, rangedWeapon); });
    }


    private void SaveItemJson()
    {
        List<Food> foodList = new List<Food>();

        foodList.Add(new Food("Bread", 3));
        foodList.Add(new Food("Apple", 1));
        foodList.Add(new Food("Steak", 5));

        JsonManager.SaveJson(foodList);

        List<Sacrifice> sacrificeList = new List<Sacrifice>();

        sacrificeList.Add(new Sacrifice("RottenApple", 1, 1));
        sacrificeList.Add(new Sacrifice("Larva", 2, 2));
        sacrificeList.Add(new Sacrifice("Boar", 4, 3));

        JsonManager.SaveJson(sacrificeList);

        List<MeleeWeapon> meleeWeaponList = new List<MeleeWeapon>();

        meleeWeaponList.Add(new MeleeWeapon("Sword", 3, 0.4f));
        meleeWeaponList.Add(new MeleeWeapon("Axe", 5, 0.6f));
        meleeWeaponList.Add(new MeleeWeapon("Knife", 1, 0.2f));

        JsonManager.SaveJson(meleeWeaponList);

        List<RangedWeapon> rangedWeaponList = new List<RangedWeapon>();

        rangedWeaponList.Add(new RangedWeapon("Wand","FireBolt", new List<Vector2>() { new Vector2(1, 0), new Vector2(0, 1) }, 0.5f));
        rangedWeaponList.Add(new RangedWeapon("Staff","FireBolt", new List<Vector2>() { new Vector2(1, 0), new Vector2(0, 1), new Vector2(1,1) }, 0.7f));
        rangedWeaponList.Add(new RangedWeapon("Stick","LightningBolt", new List<Vector2>() { new Vector2(1, 1) }, 0.4f));

        JsonManager.SaveJson(rangedWeaponList);
    }
}