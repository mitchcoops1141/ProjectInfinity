﻿using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int direction = 1;

    public GameObject playerDeath;
    public MeshRenderer mr;

    bool onContactWithWall = false;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        //move the player up or down depending on direction
        rb.velocity = transform.up * speed * direction * GameManager.instance.gameSpeed;
    }

    void Update()
    {
        //if the player presses space bar or touches the screen
        if ((Input.GetButtonDown("Jump") || Input.touchCount > 0) && rb.velocity.y == 0)
        {
            //swap direction
            direction = -direction;
           
        }

        //if player is not at x of 0 and is on a wall or roof and is not on contact with a wall
        if (transform.position.x < 0 && rb.velocity.y == 0 && !onContactWithWall)
        {
            //move player back to center
            transform.position += transform.right * speed / 2 * GameManager.instance.gameSpeed * Time.deltaTime;
        }

    }

    void Death()
    {
        Debug.Log("Player died");

        //if death cube doesnt exist
        if (GameObject.Find("PlayerDeath(Clone)") == null)
        {
            //create death cube
            Instantiate(playerDeath, transform.position, transform.rotation);
        }       

        //hide mesh for player
        mr.enabled = false;

        

        //Player death code

    }

    void OnTriggerEnter(Collider other)
    {
        //if player enters collision with wall
        if (other.CompareTag("Wall"))
        {
            //set bool to true
            onContactWithWall = true;
        }

        if (other.CompareTag("Obstacle"))
        {
            Death();
        }

       

    }

    void OnTriggerExit(Collider other)
    {
        //if player exits collision with wall
        if (other.CompareTag("Wall"))
        {
            //set bool to false
            onContactWithWall = false;
        }
    }

}
