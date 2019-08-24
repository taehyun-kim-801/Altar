using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltarUI : MonoBehaviour
{
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private GameObject recipeUIPrefab;
    [SerializeField]
    private GameObject recipeLayout;

    private List<Recipe> recipes;

    private void Start()
    {
        recipes = new List<Recipe>() { RecipeManager.Instance.GetRecipe("Apple"), RecipeManager.Instance.GetRecipe("Steak") };
        recipes.ForEach((recipe) => { Instantiate(recipeUIPrefab, recipeLayout.transform).SendMessage("SetRecipeUI", recipe); });

        exitButton.onClick.AddListener(() => CloseAltarUI());
    }

    private void CloseAltarUI()
    {
        Destroy(this);
    }
}
