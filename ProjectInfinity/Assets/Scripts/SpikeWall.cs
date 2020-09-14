using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{
    GameObject player;
    public float speed;

    bool retractBlade = false;

    void Start()
    {
        //get the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //get speed
        float s = speed * GameManager.instance.gameSpeed;

        if (player != null)
        {
            //if player is not in the middle of the screen
            if (player.transform.position.x < 0)
            {
                //move the wall to reveal the spikes
                transform.position = new Vector3(transform.position.x - s * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                //move the wall to reveal the spikes
                transform.position = new Vector3(transform.position.x + s * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }

        //if the local position is too far right
        if (transform.localPosition.x > 0.75f)
        {
            //lock it in place
            transform.localPosition = new Vector3(0.75f, 0, 0);
        }
        //if the local position is too far left
        else if (transform.localPosition.x < 0f)
        {
            //lock it in place
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
