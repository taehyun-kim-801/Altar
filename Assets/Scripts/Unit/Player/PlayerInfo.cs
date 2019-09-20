using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerInfo
{
    [SerializeField]
    public Vector3 position { get; private set; }
    [SerializeField]
    public string activeScene { get; private set; }
    [SerializeField]
    public string[] inventory { get; private set; }
    [SerializeField]
    public int[] invenQuantity { get; private set; }
    [SerializeField]
    public List<string> pinnedRecipe { get; private set; }
    [SerializeField]
    public int health { get; private set; }
    [SerializeField]
    public float hunger { get; private set; }

    public PlayerInfo(Player player)
    {
        position = player.transform.position;
        activeScene = SceneManager.GetActiveScene().name;
        inventory = player.Inventory;
        invenQuantity = player.InvenQuantity;
        pinnedRecipe = player.PinnedRecipes;
        health = player.Health;
        hunger = player.Hunger;
    }
}
