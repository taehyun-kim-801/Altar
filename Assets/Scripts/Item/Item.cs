using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Item
{
    public string name => itemName;
    public Sprite sprite { get; private set; }
    public float Delay => delay;
    public static readonly Dictionary<string, Item> itemDictionary;

    [SerializeField]
    private string itemName;
    [SerializeField]
    protected float delay = 0.5f;

    protected Item(string name)
    {
        itemName = name;
    }

    static Item()
    {
        itemDictionary = new Dictionary<string, Item>();
        JsonManager.LoadJson<Food>().ForEach((food) => { itemDictionary.Add(food.name, food); });
        JsonManager.LoadJson<Sacrifice>().ForEach((sacrifice) => { itemDictionary.Add(sacrifice.name, sacrifice); });
        JsonManager.LoadJson<MeleeWeapon>().ForEach((meleeWeapon) => { itemDictionary.Add(meleeWeapon.name, meleeWeapon); });
        JsonManager.LoadJson<RangedWeapon>().ForEach((rangedWeapon) => { itemDictionary.Add(rangedWeapon.name, rangedWeapon); });

        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("itemAtlas");
        
        foreach(var item in itemDictionary.Values)
        {
            item.sprite = spriteAtlas.GetSprite(item.name);
        }
    }

    public static void SaveItemJson()
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

        rangedWeaponList.Add(new RangedWeapon("Wand", "FireBolt", new List<Vector2>() { new Vector2(1, 0), new Vector2(0, 1) }, 0.5f));
        rangedWeaponList.Add(new RangedWeapon("Staff", "FireBolt", new List<Vector2>() { new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) }, 0.7f));
        rangedWeaponList.Add(new RangedWeapon("Stick", "LightningBolt", new List<Vector2>() { new Vector2(1, 1) }, 0.4f));

        JsonManager.SaveJson(rangedWeaponList);
    }

    public static void DropItem(string itemName, int count, Vector3 position)
    {
        GameObject gameObject = new GameObject("DroppedItem");
        gameObject = Object.Instantiate(gameObject);
        gameObject.AddComponent<DroppedItem>();
        gameObject.GetComponent<DroppedItem>().DropItem(itemDictionary[itemName], count, position);
        gameObject.tag = "DroppedItem";
    }

    public virtual void Equip(EquippedItem equipedItem) { }
}