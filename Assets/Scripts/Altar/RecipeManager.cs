using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RecipeManager : MonoBehaviour
{
    public GameObject altarUI;
    public static RecipeManager Instance { get; private set; }

    private Dictionary<string, Recipe> recipeDictionary;

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
        recipeDictionary = new Dictionary<string, Recipe>();

        LoadRecipeJson();
        Instantiate(altarUI, GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    public Recipe GetRecipe(string recipeName) => recipeDictionary[recipeName];

    private void LoadRecipeJson()
    {
        string json = File.ReadAllText($"{Application.dataPath}/Data/{nameof(Recipe)}.json");

        RecipeList recipeList = JsonUtility.FromJson<RecipeList>(json);
        recipeList.recipes.ForEach((recipe) =>
        {
            recipe.ListToDictionary();
            recipeDictionary.Add(recipe.Result, recipe);
        });
    }

    private void SaveRecipeJson()
    {
        List<Recipe> recipes = new List<Recipe>();

        recipes.Add(new Recipe(new Dictionary<string, int>() { ["RottenApple"] = 2, ["Larva"] = 1 }, "Apple"));
        recipes.Add(new Recipe(new Dictionary<string, int>() { ["Boar"] = 2, ["Larva"] = 1 }, "Steak"));

        RecipeList recipeList = new RecipeList(recipes);

        File.WriteAllText($"{Application.dataPath}/Data/{nameof(Recipe)}.json", JsonUtility.ToJson(recipeList));
    }

    private class RecipeList
    {
        public List<Recipe> recipes;

        public RecipeList(List<Recipe> recipes)
        {
            this.recipes = recipes;
        }
    }
}
