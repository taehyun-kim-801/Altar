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
    
    private List<Text> sacrificeNumbers;
    private Recipe recipe;

    private void Start()
    {
        sacrificeNumbers = new List<Text>();
        foreach(var sacrificeImage in sacrificeImages)
        {
            sacrificeNumbers.Add(sacrificeImage.GetComponentInChildren<Text>());
        }
    }

    public void SetRecipeUI(Recipe recipe)
    {
        this.recipe = recipe;
        int i = 0;
        foreach(var sacrifice in recipe.sacrifice)
        {
            sacrificeNumbers[i].text = ItemManager.Instance.GetItem(sacrifice.Key).Name;
            sacrificeImages[i].gameObject.SetActive(true);
            sacrificeImages[i++].sprite = ItemManager.Instance.GetItem(sacrifice.Key).sprite;
        }
        while(i < sacrificeImages.Count)
        {
            sacrificeImages[i].gameObject.SetActive(false);
        }
        resultImage.sprite = ItemManager.Instance.GetItem(recipe.result).sprite;
    }

    public void TradeItem()
    {
        ItemManager.Instance.DropItem(recipe.result, Vector3.zero);
    }
}