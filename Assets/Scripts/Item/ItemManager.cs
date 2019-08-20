using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [SerializeField]
    private Image itemInfoUI;
    private Dictionary<string, Item> itemDictoinary;
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
    }

    private void Start()
    {
        itemDictoinary = new Dictionary<string, Item>();

        LoadItemJson();
    }

    public void DropItem(string itemName, Vector3 position)
    {
        droppedItem.name = itemDictoinary[itemName].Name;
        droppedItem = itemDictoinary[itemName].sprite;
        Instantiate(droppedItem, position, Quaternion.Euler(0, 0, 0));
    }

    public Item GetItem(string itemName) => itemDictoinary[itemName];

    public void OpenItemInfo(string itemName)
    {
        itemInfoUI.gameObject.SetActive(true);
        itemInfoUI.SendMessage("OpenItemInfoUI", itemName);
    }

    public void CloseItemInfo()
    {
        itemInfoUI.gameObject.SetActive(false);
    }

    private void LoadItemJson()
    {
        string json = File.ReadAllText($"{Application.dataPath}/Data/{nameof(Food)}.json");

        ItemList<Food> foodList = JsonUtility.FromJson<ItemList<Food>>(json);
        foodList.items.ForEach((food) => { itemDictoinary.Add(food.Name, food); });

        json = File.ReadAllText($"{Application.dataPath}/Data/{nameof(Sacrifice)}.json");

        ItemList<Sacrifice> sacrificeList = JsonUtility.FromJson<ItemList<Sacrifice>>(json);
        sacrificeList.items.ForEach((sacrifice) => { itemDictoinary.Add(sacrifice.Name, sacrifice); });
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

        sacrifices.Add(new Sacrifice("Rotten Apple", 1, 1));
        sacrifices.Add(new Sacrifice("Larva", 2, 2));
        sacrifices.Add(new Sacrifice("Rotten Heart", 4, 3));

        ItemList<Sacrifice> sacrificeList = new ItemList<Sacrifice>(sacrifices);
        File.WriteAllText($"{Application.dataPath}/Data/{nameof(Sacrifice)}.json", JsonUtility.ToJson(sacrificeList));
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