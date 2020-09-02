using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject menuUI;
    public GameObject deathUI;

    //only 1 ui active at a time
    bool gameUIisActive;
    bool menuUIisActive;
    bool deathUIisActive;

    // Start is called before the first frame update
    void Start()
    {
        gameUIisActive = false;
        menuUIisActive = true;
        deathUIisActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameUIisActive)
        {
            GameManager.instance.gameSpeed = 1;

            if (GameObject.Find("Player") == null)
            {
                gameUIisActive = false;
                deathUIisActive = true;
            }
        }

        if (menuUIisActive)
        {
            GameManager.instance.gameSpeed = 0;

            if (Input.GetButtonDown("Jump"))
            {
                menuUIisActive = false;
                gameUIisActive = true;
            }
        }

        if (deathUIisActive)
        {
            GameManager.instance.gameSpeed = 0;
        }
    }
}
