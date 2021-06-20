using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioClip hitClip;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
        }   
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(hitClip);
    }
}
