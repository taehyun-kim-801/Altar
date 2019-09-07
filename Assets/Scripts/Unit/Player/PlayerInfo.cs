using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerInfo
{
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private string activeScene;
    [SerializeField]
    private string[] inventory;
    [SerializeField]
    private List<string> pinnedRecipe;
    [SerializeField]
    private int health;
    [SerializeField]
    private float hunger;
    [SerializeField]
    private Item item;

    public PlayerInfo(Player player)
    {
        position = player.transform.position;
        activeScene = SceneManager.GetActiveScene().name;
        inventory = player.Inventory;
        pinnedRecipe = player.PinnedRecipes;
        health = player.Health;
        hunger = player.Hunger;
        //item = player.EquippedItem.selectedItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
