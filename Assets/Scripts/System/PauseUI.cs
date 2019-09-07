using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    private GameObject OptionUI;
    [SerializeField]
    private GameObject GuideBookUI;

    public void OpenOptionUI()
    {
        OptionUI.SetActive(true);
    }

    public void OpenGuideBookUI()
    {
        GuideBookUI.SetActive(true);
    }

    public void ClosePauseUI()
    {
        gameObject.SetActive(false);
    }

    public void CloseGuideBookUI()
    {
        GuideBookUI.SetActive(false);
    }

    public void CloseOptionUI()
    {
        OptionUI.SetActive(false);
    }
}
