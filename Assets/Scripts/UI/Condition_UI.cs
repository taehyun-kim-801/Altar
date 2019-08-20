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
    private Image[] health;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<Player>();
        health = healthBar.GetComponentsInChildren<Image>();
    }

    public void HealthUI()
    {
        Debug.Log(health.Length);
        for (int i = 0; i < health.Length; i++)
        {
            health[i].sprite = heart[Mathf.Clamp(2 - (player.Health - i * 2), 0, 2)];
        }
    }
}
