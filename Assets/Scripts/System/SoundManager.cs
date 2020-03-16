using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [SerializeField]
    private AudioSource efxSource;
    [SerializeField]
    private AudioSource musicSource;

    public bool isMute { get; private set; }
    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        LoadSound();
    }

    private void LoadSound()
    {
        Object[] obj= Resources.LoadAll("Sounds");
        foreach(var audio in obj)
        {
            if(audio is AudioClip)
                soundDictionary.Add(audio.name ,(AudioClip)audio);
        }
    }

    public bool TryPlayingEffect(string audioName)
    {
        if(!soundDictionary.ContainsKey(audioName))
            return false;
        
        efxSource.clip = soundDictionary[audioName];
        efxSource.Play();
        return true;
    }

    public bool TryPlayingMusic(string audioName)
    {
        if(!soundDictionary.ContainsKey(audioName))
            return false;
        
        musicSource.clip = soundDictionary[audioName];
        musicSource.Play();
        return true;
    }

    public void Mute()
    {
        isMute = true;
        efxSource.volume = 0.0f;
        musicSource.volume = 0.0f;
    }

    public void Unmute()
    {
        isMute = false;
        efxSource.volume = 1.0f;
        musicSource.volume = 1.0f;
    }
}