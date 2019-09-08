using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public Dictionary<string, int> sacrificeDictionary {get; private set;}
    public string Result => result;
    public static readonly Dictionary<string, Recipe> recipeDictionary;

    [SerializeField]
    private List<string> sacrificeNameList;
    [SerializeField]
    private List<int> sacrificeCountList;
    [SerializeField]
    private string result;

    public Recipe(Dictionary<string, int> sacrifice, string result)
    {
        sacrificeDictionary = sacrifice;
        sacrificeNameList = new List<string>();
        sacrificeCountList = new List<int>();
        foreach(var s in sacrifice)
        {
            sacrificeNameList.Add(s.Key);
            sacrificeCountList.Add(s.Value);
        }
        this.result = result;
    }

    static Recipe()
    {
        recipeDictionary = new Dictionary<string, Recipe>();
        JsonManager.LoadJson<Recipe>().ForEach((recipe) =>
        {
            recipe.ListToDictionary();
            recipeDictionary.Add(recipe.Result, recipe);
        });
    }

    public void ListToDictionary()
    {
        sacrificeDictionary = new Dictionary<string, int>();
        for(int i = 0; i<sacrificeNameList.Count; i++)
            sacrificeDictionary.Add(sacrificeNameList[i], sacrificeCountList[i]);
    }

    private static void SaveRecipeJson()
    {
        List<Recipe> recipes = new List<Recipe>();

        recipes.Add(new Recipe(new Dictionary<string, int>() { ["RottenApple"] = 2, ["Larva"] = 1 }, "Apple"));
        recipes.Add(new Recipe(new Dictionary<string, int>() { ["Boar"] = 2, ["Larva"] = 1 }, "Steak"));

        JsonManager.SaveJson(recipes);
    }
}
