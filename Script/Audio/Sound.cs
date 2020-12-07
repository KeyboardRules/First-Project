using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0,1f)]
    public float volumn;
    [Range(0, 1f)]
    public float pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
