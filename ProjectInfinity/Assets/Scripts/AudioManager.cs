using System;
using System.Collections;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public Sound[] sounds;


    // Start is called before the first frame update
    void Awake()
    {
        //checking if there is a audio manager 
        if (instance == null)
            //make this the audio manager
            instance = this;
        //if we load a new level and this is not the current instance of the audio manager
        else if (instance != this)
            //destroy the new script
            Destroy(gameObject);

        //dont destroy this gameobject the script is attatched to
        DontDestroyOnLoad(gameObject);

        //for each sound in the sounds array
        foreach (Sound s in sounds)
        {
            //set all features of Sound class to the audiosource componant
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    public float GetLength(string name)
    {
        //find sound in array via searching with name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //if no sound with given name
        if (s == null)
            //return
            return 0;

        //return volume
        return s.source.clip.length;
    }

    public float GetVolume(string name)
    {
        //find sound in array via searching with name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //if no sound with given name
        if (s == null)
            //return
            return 0;

        //return volume
        return s.source.volume;
    }

    public void SetVolume(string name, float volume)
    {
        //find sound in array via searching with name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //if no sound with given name
        if (s == null)
            //return
            return;

        //change volume
        s.source.volume = volume;
    }

    // Update is called once per frame
    public void Play(string name)
    {
        //find sound in array via searching with name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //if no sound with given name
        if (s == null)
            //return
            return;
        //play given sound
        s.source.Play();
    }

    public void PlayOneShot(string name)
    {
        //find sound in array via searching with name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //if no sound with given name
        if (s == null)
            //return
            return;
        //play given sound with given volume
        s.source.PlayOneShot(s.clip);
    }

    public void Stop(string name)
    {
        //find sound in array via searching with name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //if no sound with given name
        if (s == null)
            //return
            return;
        //stop playing given sound
        s.source.Stop();
    }
}
