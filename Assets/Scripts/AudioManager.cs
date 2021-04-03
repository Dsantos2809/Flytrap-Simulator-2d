using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            Play("MainTheme");
        }
        else Stop("MainTheme");
        Play("Fly");
        Play("Bee");
        Play("Beetle");
        Play("Mosquito");
        Play("Base");
        Volume("Fly", 0f);
        Volume("Bee", 0f);
        Volume("Beetle", 0f);
        Volume("Mosquito",0f);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Play("MainTheme");
            Volume("Fly", 0f);
            Volume("Bee", 0f);
            Volume("Beetle", 0f);
            Volume("Mosquito", 0f);
            Volume("Base", 0f);
        }
        else
        {
            Stop("MainTheme");
            Volume("Base", 1f);
        }
    }

    public void Volume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.volume = volume;
        s.volume = s.source.volume;
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        if (!s.source.isPlaying)s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        if (s.source.isPlaying) s.source.Stop();
    }
}
