using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public float gameSpeed = 1;


    // Start is called before the first frame update
    void Awake()
    {
        //checking if there is a game manager 
        if (instance == null)
            //make this the game manager
            instance = this;
        //if we load a new level and this is not the current instance of the game manager
        else if (instance != this)
            //destroy the new script
            Destroy(gameObject);

        //dont destroy this gameobject the script is attatched to
        DontDestroyOnLoad(gameObject);
    }

    float timer = 1;
    // Update is called once per frame
    void Update()
    {
        //calculate time
        timer += Time.deltaTime;
        //calc mins
        //calc secs
        float mins = Mathf.FloorToInt(timer % 60);
        //gameSpeed = mins + 1;
    }
}
