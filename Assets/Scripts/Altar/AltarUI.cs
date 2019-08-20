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
    [SerializeField]
    private GameObject itemInfoPrefab;

    private List<Recipe> recipes;

    private void Start()
    {
        // 레시피 정보 받아오기
        exitButton.onClick.AddListener(() => CloseAltarUI());
    }

    private void CloseAltarUI()
    {
        Destroy(this);
    }

    private void MakeRecipeUI()
    { 
        foreach(Recipe recipe in recipes)
        {
            Instantiate(recipeUIPrefab,recipeLayout.transform).SendMessage("SetRecipeUI", recipe);
        }
    }
}
