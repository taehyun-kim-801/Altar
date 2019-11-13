using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinnedRecipeUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> sacrificeImages;
    [SerializeField]
    private Image resultImage;
    [SerializeField]
    private Button pinButton;

    private Recipe recipe;

    // Start is called before the first frame update
    void Start()
    {
        pinButton.onClick.AddListener(() => RemoveRecipe());
    }

    public void SetPinnedRecipeUI(Recipe recipe)
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

    private void RemoveRecipe()
    {
        Player.Instance.RemoveRecipe(recipe.Result);
        Destroy(gameObject);
    }
}
