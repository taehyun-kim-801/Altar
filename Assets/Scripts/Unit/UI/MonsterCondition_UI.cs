using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCondition_UI : MonoBehaviour
{
    public static MonsterCondition_UI Instance { get; private set; }

    public Image healthBar;
    public Image monsterImage;

    private bool settingActive = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(this);
            return;
        }

        gameObject.SetActive(false);
    }

    public void SetMonsterCondition(string name,float healthPercent)
    {
        if (settingActive) settingActive = false;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        monsterImage.sprite = DataContainer.monsterAtlas.GetSprite($"{name}_1");
        monsterImage.preserveAspect = true;
        healthBar.fillAmount = healthPercent;

        if(healthPercent<=0f)
        {
            StartCoroutine(Inactive());
        }
    }

    public IEnumerator Inactive()
    {
        settingActive = true;
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        settingActive = false;
    }
}
