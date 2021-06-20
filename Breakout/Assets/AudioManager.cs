using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
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
            sound.source.clip = sound.clip;
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
        s.source.PlayOneShot(s.clip);
    }
}
