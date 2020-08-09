using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Audio[] audios;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Audio s in audios)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
            
        }
    }

    void Start()
    {
        Play("Theme");
    }

    public void PlayButtonClick()
    {
        Play("Button");
    }

    public void Play(string name)
    {
        foreach(Audio s in audios)
        {
            if (s.name == name)
            {
                s.source.Play();                
            }
        }        
    }
}
