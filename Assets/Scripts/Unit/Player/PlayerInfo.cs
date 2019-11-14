using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerInfo
{
    [SerializeField]
    public float posX { get; private set; }
    [SerializeField]
    public float posY { get; private set; }
    [SerializeField]
    public string activeScene { get; private set; }
    [SerializeField]
    public string[] inventory { get; private set; }
    [SerializeField]
    public int[] invenQuantity { get; private set; }
    [SerializeField]
    public string[] pinnedRecipe { get; private set; }
    [SerializeField]
    public int health { get; private set; }
    [SerializeField]
    public float hunger { get; private set; }

    public PlayerInfo(Player player)
    {
        posX = player.transform.position.x;
        posY = player.transform.position.y;
        activeScene = SceneManager.GetActiveScene().name;
        inventory = player.Inventory;
        invenQuantity = player.InvenQuantity;
        pinnedRecipe = player.PinnedRecipes.ToArray();
        health = player.Health;
        hunger = player.Hunger;
    }
}
