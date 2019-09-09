using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition_UI : MonoBehaviour
{
    public GameObject playerObj;
    private Player player;

    public Sprite[] heart;
    public GameObject healthBar;
    public GameObject hungerBar;
    private Image[] health;
    private Image hungerImage;
    private Text hungerText;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<Player>();
        health = healthBar.GetComponentsInChildren<Image>();
        hungerImage = hungerBar.GetComponent<Image>();
        hungerText = hungerBar.GetComponentInChildren<Text>();
    }

    public void HealthUI()
    {
        Debug.Log(health.Length);
        for (int i = 0; i < health.Length; i++)
        {
            health[i].sprite = heart[Mathf.Clamp(2 - (player.Health - i * 2), 0, 2)];
        }
    }

    public void HungerUI()
    {
        hungerImage.fillAmount = player.Hunger / 100f;
        hungerText.text = string.Format("{0:f1} / 100.0", player.Hunger);
    }
}
