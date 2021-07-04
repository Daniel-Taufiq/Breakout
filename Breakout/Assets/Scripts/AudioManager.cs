using UnityEngine.Audio;
using System;
using UnityEngine;
using Random=UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider musicVolumePause;
    public Sound[] sounds;
    private Sound bg;   // background sound
    public bool soundEffects;
    private bool toggle;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        // check to make sure we don't duplicate AudioManager's between scenes
        if(instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
            return;
        }
        // for transitioning scenes: keep sounds playing
        DontDestroyOnLoad(gameObject);

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip[0];
        }
        
    }

    void Start()
    {
        string name = "BackgroundAudio";
        bg = Array.Find(sounds, sound => sound.name == name);
        // no saved volume data
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else if(!PlayerPrefs.HasKey("musicVolumePause"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    void Update()
    {
        //AudioListener.volume = musicVolume;
        musicVolume.value = bg.source.volume;
        musicVolumePause.value = bg.source.volume;
        if(!bg.source.isPlaying)
        {
            bg.source.PlayOneShot(bg.clip[Random.Range(0, bg.clip.Length)]);
        }

    }

    public void updateMusicVolume()
    {
        bg.source.volume = musicVolume.value;
        Save("menu");
    }

    public void updateMusicVolumePause()
    {
        bg.source.volume = musicVolumePause.value;
        Save("pause");
    }

    private void Load()
    {
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save(string value)
    {
        if(value == "pause")
        {
            PlayerPrefs.SetFloat("musicVolumePause", musicVolume.value);
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }

        if(soundEffects == true)
        {
            s.source.PlayOneShot(s.clip[0]);
        }
    }


    public void SoundEffect(bool soundActive)
    {
        Debug.Log(soundActive);
        soundEffects = soundActive;
        string name = "Toggle";
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.clip[0]);
    }
}
