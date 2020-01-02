using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject muteButton;
    [SerializeField]
    private GameObject unMuteButton;

    private void Start()
    {
        if (GameManager.Instance.Mute)
            Mute();
        else
            UnMute();
    }

    public void Mute()
    {
        muteButton.SetActive(false);
        unMuteButton.SetActive(true);
        SoundManager.instance.Mute();
    }

    public void UnMute()
    {
        unMuteButton.SetActive(false);
        muteButton.SetActive(true);
        SoundManager.instance.Unmute();
    }

    public void CloseOptionUI()
    {
        gameObject.SetActive(false);
    }
}
