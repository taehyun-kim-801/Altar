using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltarUI : MonoBehaviour
{
    public event System.Action close;

    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private GameObject recipeUIPrefab;
    [SerializeField]
    private GameObject recipeLayout;

    private List<Recipe> recipes;

    private void Start()
    {
        System.Action SetTradeButtonAction = null;
        List<RecipeUI> recipeUIList = new List<RecipeUI>();
        recipes = new List<Recipe>() { Recipe.recipeDictionary["Apple"], Recipe.recipeDictionary["Steak"] };

        recipes.ForEach((recipe) =>
        {
            GameObject recipeUI = Instantiate(recipeUIPrefab, recipeLayout.transform);
            recipeUI.SendMessage("SetRecipeUI", recipe);
            SetTradeButtonAction += recipeUI.GetComponent<RecipeUI>().SetTradeButton;
            recipeUIList.Add(recipeUI.GetComponent<RecipeUI>());
        });

        recipeUIList.ForEach((recipeUI) =>
        {
            recipeUI.setTradeButtonAction += SetTradeButtonAction;
        });

        exitButton.onClick.AddListener(() => CloseAltarUI());
    }

    private void CloseAltarUI()
    {
        close();
        Destroy(gameObject);
    }
}
