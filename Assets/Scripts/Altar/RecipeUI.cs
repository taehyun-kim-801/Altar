using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> sacrificeImages;
    [SerializeField]
    private Image resultImage;
    [SerializeField]
    private Button pinButton;
    [SerializeField]
    private Button tradeButton;

    private Recipe recipe;

    public void SetRecipeUI(Recipe recipe)
    {
        this.recipe = recipe;
        int i = 0;
        foreach (var sacrifice in recipe.sacrificeDictionary)
        {
            if (i >= sacrificeImages.Count)
            {
                Debug.Log("제물을 표현할 공간이 부족합니다.");
                break;
            }
            sacrificeImages[i].GetComponent<ItemCell>().SetItemCell(sacrifice.Key, sacrifice.Value);
            sacrificeImages[i++].gameObject.SetActive(true);
        }
        while (i < sacrificeImages.Count)
        {
            sacrificeImages[i++].gameObject.SetActive(false);
        }
        resultImage.GetComponent<ItemCell>().SetItemCell(recipe.Result, 1);
    }

    public void TradeItem()
    {

        // ItemManager.Instance.DropItem(recipe.Result, );
    }
}