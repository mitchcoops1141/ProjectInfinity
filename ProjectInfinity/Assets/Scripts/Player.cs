using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int direction = 1;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if the player presses space bar or touches the screen
        if ((Input.GetButtonDown("Jump") || Input.touchCount > 0) && rb.velocity.y == 0)
        {
            //swap direction
            direction = -direction;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //move the player up or down depending on direction
        rb.velocity = transform.up * speed * direction;
    }
}
