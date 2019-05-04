using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public static float generalVolume = 1f;
    public static float sfxVolume = 1f;
    public static float musicVolume = 1f;
    public Sound[] sounds;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

    private void Update()
    {
        foreach(Sound s in sounds)
        {
            if(s.music)
                s.source.volume = s.volume * generalVolume * musicVolume;
            else
                s.source.volume = s.volume * generalVolume * sfxVolume;
        }
    }


    public void Play (string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }

        s.source.volume = s.volume * generalVolume;
        if (!s.music)
        {
            s.source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        }
        else s.source.pitch = 1f;
        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.Stop();
    }
}
