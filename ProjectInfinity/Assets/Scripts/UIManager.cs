using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject menuUI;
    public GameObject deathUI;
    public GameObject pauseUI;

    public GameObject scoreObj;
    public GameObject pauseScoreObj;

    float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //on start. menu is active. game and death and pause are not
        deathUI.SetActive(false);
        gameUI.SetActive(false);
        menuUI.SetActive(true);
        pauseUI.SetActive(false);
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

            //gamespeed = 0
            Time.timeScale = 0.0f;
        }
    }
}
