using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header ("GAME SPEED")]
    public float gameSpeed = 1;
    public float gameSpeedIncreaseRate = 0.01f;
    public float maxGameSpeed = 2.5f;

    [Header("COLORS")]
    public Material background;
    public Material wall;
    public Material strip;
    public Material spike;
    public Material levelWall;

    //color index: 0 = blue. 1 = pink. 2 = red. 3 = yellow. 4 = green
    public Color[] backgroundColors;
    public Color[] wallColors;
    public float waitTime = 5;

    bool shouldChangeColor = true;

    Color nextColorBackground;
    Color nextColorWall;
    int colorIndex = 0;
    

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

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

    
    void Start()
    {
        //reset game speed
        gameSpeed = 1;

        //set the background color
        background.color = backgroundColors[colorIndex];
        //set the next background color to b current background color
        nextColorBackground = backgroundColors[colorIndex];

        //set the wall color
        wall.color = wallColors[colorIndex];
        //set the next wall color to be the current wall color
        nextColorWall = wallColors[colorIndex];
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

        //transition the background color the next background color
        background.color = Color.Lerp(background.color, nextColorBackground, 0.005f);
        //transitionthe wall color to the next wall color
        wall.color = Color.Lerp(wall.color, nextColorWall, 0.005f);

        //if we should transition the color
        if (shouldChangeColor)
        {
            //run the change color function
            StartCoroutine("ColorChange");
        }

        //set the spikes to be the same color as the walls
        spike.color = wall.color;
        //set the strips to be the same color as the background
        strip.color = wall.color;
        //set the level walls to be the same as the wall
        levelWall.color = wall.color;
    }

    IEnumerator ColorChange()
    {
        //ensure function doesnt get called again
        shouldChangeColor = false;

        //wait specified time
        yield return new WaitForSeconds(waitTime);

        colorIndex = UnityEngine.Random.Range(0, backgroundColors.Length - 1);

        //increase the speed
        GameManager.instance.gameSpeed += gameSpeedIncreaseRate;

        //if the speed goes past max
        if (GameManager.instance.gameSpeed > maxGameSpeed)
        {
            //set speed to max
            GameManager.instance.gameSpeed = maxGameSpeed;
        }

        //set the next colors based off the index
        nextColorBackground = backgroundColors[colorIndex];
        nextColorWall = wallColors[colorIndex];

        //allow this function to be claled again
        shouldChangeColor = true;
    }
}
