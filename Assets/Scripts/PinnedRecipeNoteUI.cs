using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinnedRecipeNoteUI : MonoBehaviour
{
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private GameObject recipeUIPrefab;
    [SerializeField]
    private GameObject recipeLayout;

    private void Start()
    {
        List<RecipeUI> recipeUIList = new List<RecipeUI>();
        List<Recipe> recipes = new List<Recipe>();
        Player.Instance.PinnedRecipes.ForEach(recipe =>
        {
            var recipeUI = Instantiate(recipeUIPrefab, recipeLayout.transform) as GameObject;
            recipeUI.SendMessage("SetPinnedRecipeUI", Recipe.recipeDictionary[recipe]);
            recipeUIList.Add(recipeUI.GetComponent<RecipeUI>());
        });
        exitButton.onClick.AddListener(() => ClosePinnedRecipeUI());
    }

    public void ClosePinnedRecipeUI()
    {
        Destroy(gameObject);
    }
}
