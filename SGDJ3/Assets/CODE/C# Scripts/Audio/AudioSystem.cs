using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioSystem : Singleton<AudioSystem>
{
    public Sound[] sounds;
    public AudioSource mainMusic;
    public AudioSource soundFX;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            if (s.soundFX)
            {
                soundFX.clip = s.clip;
                soundFX.volume = s.volume;                
                soundFX.Play();
            }
            else
            {
                mainMusic.clip = s.clip;
                mainMusic.volume = s.volume;
                mainMusic.loop = s.loop;
                mainMusic.playOnAwake = s.playOnAwake;
                mainMusic.Play();
            }
        }
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            mainMusic.clip = s.clip;
            mainMusic.Stop();
        }
    }
}
