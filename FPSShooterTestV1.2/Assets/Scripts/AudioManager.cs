using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            //   s.source.outputAudioMixerGroup = s.mixer; Sound' does not contain a definition for 'mixer' and no accessible extension method 'mixer' accepting a first argument of type 'Sound' could be found (are you missing a using directive or an assembly reference?)
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.Play();
    }
    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.Stop();
    }
}

