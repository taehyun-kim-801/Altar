using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClip[] audioClip;
    public Dictionary<string, AudioClip> audioClipsDic;

    private AudioSource sfxPlayer;
    private float masterVolumeSFX = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        sfxPlayer = GetComponent<AudioSource>();

        audioClipsDic = new Dictionary<string, AudioClip>();
        foreach (AudioClip a in audioClip)
        {
            audioClipsDic.Add(a.name, a);
        }
        SetVolumeSFX(GameManager.Instance.Mute ? 0 : 1);
    }

    public void PlaySound(string a_name, float a_volume = 1f)
    {
        if (audioClipsDic.ContainsKey(a_name) == false)
        {
            Debug.Log(a_name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[a_name], a_volume * masterVolumeSFX);
    }

    public void SetVolumeSFX(float a_volume)
    {
        masterVolumeSFX = a_volume;
    }
}
