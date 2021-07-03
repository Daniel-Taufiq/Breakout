using UnityEngine.Audio;
using System;
using UnityEngine;
using Random=UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private Sound bg;   // background sound
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
    }

    void Update()
    {
        if(!bg.source.isPlaying)
        {
            bg.source.PlayOneShot(bg.clip[Random.Range(0, bg.clip.Length)]);
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
        s.source.PlayOneShot(s.clip[0]);
    }

    public void ChangeVolume(string upOrDown)
    {
        if(upOrDown == "up")
        {
            AudioListener.volume = 1.0f;
        }
        else
        {
            AudioListener.volume = 0.1f;
        }
    }
}
