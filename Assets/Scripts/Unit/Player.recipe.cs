using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private List<string> pinnedRecipes;
    public List<string> PinnedRecipes => pinnedRecipes;

    public void AddRecipe(string recipeResult)
    {
        pinnedRecipes.Add(recipeResult);
    }

    public void RemoveRecipe(string recipeResult)
    {
        pinnedRecipes.Remove(recipeResult);
    }
}
