using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    private GameObject optionUI;
    [SerializeField]
    private GameObject guideBookUI;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void OpenOptionUI()
    {
        optionUI.SetActive(true);
    }

    public void OpenGuideBookUI()
    {
        guideBookUI.SetActive(true);
    }

    public void ClosePauseUI()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
