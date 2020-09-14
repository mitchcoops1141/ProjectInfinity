using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("TESTING")]
    public bool testMode;
    public GameObject testLevel;

    [Header("LEVELS")]
    public GameObject[] levels;

    public static LevelManager instance = null;

    void Awake()
    {
        //checking if there is a level manager 
        if (instance == null)
            //make this the level manager
            instance = this;
        //if we load a new level and this is not the current instance of the level manager
        else if (instance != this)
            //destroy the new script
            Destroy(gameObject);

        //dont destroy this gameobject the script is attatched to
        DontDestroyOnLoad(gameObject);
    }
}
