using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCondition_UI : MonoBehaviour
{
    public static MonsterCondition_UI Instance { get; private set; }

    private Image healthBar;
    private Image monsterImage;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(this);
            return;
        }

        healthBar = GetComponentsInChildren<Image>()[1];
        monsterImage = GetComponentInChildren<Image>().gameObject.GetComponentInChildren<Image>();
        gameObject.SetActive(false);
    }

    public void SetMonsterCondition(string name,float healthPercent)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        monsterImage.sprite = DataContainer.monsterAtlas.GetSprite($"{name}_1");
        healthBar.fillAmount = healthPercent;
    }
}
