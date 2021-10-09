using UnityEngine;
using System;

public class AudioSystem : Singleton<AudioSystem>
{
    public Sound[] sounds;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }
    }
    

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.Log("No existe el sonido " + name);
        }
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.Log("No existe el sonido " + name);
        }
    }
}
