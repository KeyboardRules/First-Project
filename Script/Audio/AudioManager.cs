using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;

    public static Sound currentTheme;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volumn;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void PlaySound(string name)
    {
        Sound s=Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("not found the sound");
            return;
        }
        s.source.Play();
    }
    public void ChangeSceneTheme(string name)
    {
        if (currentTheme != null)
        {
            currentTheme.source.Stop();
        }
        currentTheme = Array.Find(sounds, sounds => sounds.name == name);
        if (currentTheme == null)
        {
            currentTheme.source.clip = currentTheme.clip;
        }
        currentTheme.source.Play();
    }
    public void ChangeVolumn(float value)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = value;
            s.volumn = value;
        }
    }
}
