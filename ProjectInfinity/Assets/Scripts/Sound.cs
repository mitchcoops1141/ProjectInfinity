using UnityEngine.Audio;
using UnityEngine;

//show in inspector
[System.Serializable]

//CUSTOM SOUND CLASS
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    [Range(0f, 1f)]
    public float spatialBlend;
}
