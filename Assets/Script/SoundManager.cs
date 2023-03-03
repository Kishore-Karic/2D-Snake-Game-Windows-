using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public AudioSource soundEffect;
    public AudioSource soundMusic;

    public SoundType[] Sounds;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEffect(Sounds sound)
    {
        AudioClip clip = GetAudioClip(sound);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
    }

    public void PlayMusic(Sounds sound)
    {
        AudioClip clip = GetAudioClip(sound);
        if(clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        }
    }

    public void StopMusic(Sounds sound)
    {
        AudioClip clip = GetAudioClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Stop();
        }
    }

    private AudioClip GetAudioClip(Sounds sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if(item != null)
        {
            return item.soundClip;
        }
        else
        {
            return null;
        }
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

public enum Sounds
{
    LobbyTheme,
    GameTheme,
    Win,
    Lose,
    Food,
    AntiFood,
    PowerUp,
    ButtonClick
}
