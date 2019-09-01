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
        string json = File.ReadAllText($"{Application.dataPath}/Data/{nameof(Food)}.json");
        JsonUtility.FromJson<ItemList<Food>>(json).items.ForEach((food) => { itemDictionary.Add(food.ItemName, food); });

        json = File.ReadAllText($"{Application.dataPath}/Data/{nameof(Sacrifice)}.json");
        JsonUtility.FromJson<ItemList<Sacrifice>>(json).items.ForEach((sacrifice) => { itemDictionary.Add(sacrifice.ItemName, sacrifice); });

        json = File.ReadAllText($"{Application.dataPath}/Data/{nameof(MeleeWeapon)}.json");
        JsonUtility.FromJson<ItemList<MeleeWeapon>>(json).items.ForEach((meleeWeapon) => { itemDictionary.Add(meleeWeapon.ItemName, meleeWeapon); });

        json = File.ReadAllText($"{Application.dataPath}/Data/{nameof(RangedWeapon)}.json");
        JsonUtility.FromJson<ItemList<RangedWeapon>>(json).items.ForEach((rangedWeapon) => { itemDictionary.Add(rangedWeapon.ItemName, rangedWeapon); });
    }


    private void SaveItemJson()
    {
        List<Food> foods = new List<Food>();

        foods.Add(new Food("Bread", 3));
        foods.Add(new Food("Apple", 1));
        foods.Add(new Food("Steak", 5));

        ItemList<Food> foodList = new ItemList<Food>(foods);
        File.WriteAllText($"{Application.dataPath}/Data/{nameof(Food)}.json", JsonUtility.ToJson(foodList));

        List<Sacrifice> sacrifices = new List<Sacrifice>();

        sacrifices.Add(new Sacrifice("RottenApple", 1, 1));
        sacrifices.Add(new Sacrifice("Larva", 2, 2));
        sacrifices.Add(new Sacrifice("Boar", 4, 3));

        ItemList<Sacrifice> sacrificeList = new ItemList<Sacrifice>(sacrifices);
        File.WriteAllText($"{Application.dataPath}/Data/{nameof(Sacrifice)}.json", JsonUtility.ToJson(sacrificeList));

        List<MeleeWeapon> meleeWeapons = new List<MeleeWeapon>();

        meleeWeapons.Add(new MeleeWeapon("Sword", 3, 0.4f));
        meleeWeapons.Add(new MeleeWeapon("Axe", 5, 0.6f));
        meleeWeapons.Add(new MeleeWeapon("Knife", 1, 0.2f));

        ItemList<MeleeWeapon> meleeWeaponList = new ItemList<MeleeWeapon>(meleeWeapons);
        File.WriteAllText($"{Application.dataPath}/Data/{nameof(MeleeWeapon)}.json", JsonUtility.ToJson(meleeWeaponList));

        List<RangedWeapon> rangedWeapons = new List<RangedWeapon>();

        rangedWeapons.Add(new RangedWeapon("Wand","FireBolt", new List<Vector2>() { new Vector2(1, 0), new Vector2(0, 1) }, 0.5f));
        rangedWeapons.Add(new RangedWeapon("Staff","FireBolt", new List<Vector2>() { new Vector2(1, 0), new Vector2(0, 1), new Vector2(1,1) }, 0.7f));
        rangedWeapons.Add(new RangedWeapon("Stick","LightningBolt", new List<Vector2>() { new Vector2(1, 1) }, 0.4f));

        ItemList<RangedWeapon> rangedWeaponList = new ItemList<RangedWeapon>(rangedWeapons);
        File.WriteAllText($"{Application.dataPath}/Data/{nameof(RangedWeapon)}.json", JsonUtility.ToJson(rangedWeaponList));
    }

    private class ItemList<T> where T : Item
    {
        public List<T> items;

        public ItemList(List<T> ts)
        {
            items = ts;
        }
    }
}