using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("GAME UI")]
    public GameObject gameUI;
    public GameObject scoreObj;

    [Header("MENU UI")]
    public GameObject menuUI;
    public GameObject highScoreTextObj;

    [Header("DEATH UI")]
    public GameObject deathUI;
    public GameObject deathScorePromptTextObj;
    public GameObject deathScoreTextObj;

    [Header("PAUSE UI")]
    public GameObject pauseUI;
    public GameObject pauseScoreObj;


    float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //set highscore text to the highscore player pref
        highScoreTextObj.GetComponent<TextMeshProUGUI>().SetText("HIGHSCORE: " + PlayerPrefs.GetFloat("Highscore", 0));

        //on start. menu is active. game and death and pause are not
        deathUI.SetActive(false);
        gameUI.SetActive(false);
        menuUI.SetActive(true);
        pauseUI.SetActive(false);

        //set score to 0
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if gmae ui is active
        if (gameUI.activeSelf)
        {
            //deaht and menu ui deactive
            deathUI.SetActive(false);
            menuUI.SetActive(false);
            pauseUI.SetActive(false);

            //if cant find the player
            if (GameObject.Find("Player") == null)
            {
                deathUI.SetActive(true);
            }
            //if can find the player
            else
            {
                //add to score
                score += Time.deltaTime * 10 * GameManager.instance.gameSpeed;
                score = Mathf.Round(score * 100f) / 100f;
                //set score text
                scoreObj.GetComponent<TextMeshProUGUI>().SetText("SCORE: " + score);
            }

            //if escape is presed OR pause button is pressed
            if (Input.GetButtonDown("Pause"))
            {
                //set pause screen actvie
                pauseUI.SetActive(true);
            }

        }

        //if on pause screen
        if (pauseUI.activeSelf)
        {
            //set game ui to deactive
            gameUI.SetActive(false);

            //freeze time
            Time.timeScale = 0.0f;

            //set the text for the paused score object
            pauseScoreObj.GetComponent<TextMeshProUGUI>().SetText("SCORE: " + score);

            //if press space, or touch screen, or press pause button, or press escape
            if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
            {
                //reset time and game
                Time.timeScale = 1.0f;

                //set game ui to active
                gameUI.SetActive(true);
            }
        }

        //if menu is active
        if (menuUI.activeSelf)
        {
            //death and game ui deactive
            deathUI.SetActive(false);
            gameUI.SetActive(false);

            //game speed = 0
            Time.timeScale = 0.0f;

            //if the player hits space or touches screen
            if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
            {
                //game starts
                Time.timeScale = 1.0f;
                menuUI.SetActive(false);
                gameUI.SetActive(true);
            }
        }

        //if death menu is active
        if (deathUI.activeSelf)
        {            
            //game and menu not active
            gameUI.SetActive(false);
            menuUI.SetActive(false);

            //if score is bigger then current high score
            if (score > PlayerPrefs.GetFloat("Highscore"))
            {
                //set text to for new highscore
                deathScorePromptTextObj.GetComponent<TextMeshProUGUI>().SetText("NEW HIGHSCORE:");
            }
            //if score is less then high score
            else
            {
                //set text to for new highscore
                deathScorePromptTextObj.GetComponent<TextMeshProUGUI>().SetText("SCORE:");
            }

            //show the score
            deathScoreTextObj.GetComponent<TextMeshProUGUI>().SetText(score.ToString());

            //gamespeed = 0
            Time.timeScale = 0.0f;
        }
    }

    public void Back()
    {
        //if the current score is bigger then the highscore
        if (score > PlayerPrefs.GetFloat("Highscore"))
        {
            //update the highscore
            PlayerPrefs.SetFloat("Highscore", score);
        }

        //reset the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
