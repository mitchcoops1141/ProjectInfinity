using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("GENERAL VARIABLES")]
    public float speed;
    public int direction = 1;

    [Header("CONNECTIONS")]
    public GameObject playerDeath;
    public MeshRenderer mr;
    public Animation deathFlash;

    [Header("CAMERA SHAKE")]
    public Transform cam;
    public float shakeAmount;
    public float shakeTime;

    //private varibles
    bool onContactWithWall = false;

    bool isShaking = false;
    bool isDead = false;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;

        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        //move the player up or down depending on direction
        rb.velocity = transform.up * speed * direction * GameManager.instance.gameSpeed;
    }

    void Update()
    {
        //if the player presses space bar or touches the screen and the player is not moving up or down, and the game is not paused
        if ((Input.GetButtonDown("Jump") || Input.touchCount > 0) && rb.velocity.y == 0 && Time.timeScale == 1f)
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

    IEnumerator Death()
    {
        isDead = true;

        //if death cube doesnt exist
        if (GameObject.Find("PlayerDeath(Clone)") == null)
        {
            //create death cube
            Instantiate(playerDeath, transform.position, transform.rotation);
        }

        //if not already shaking
        if (!isShaking)
            //play the death falsh animation
            deathFlash.Play();

        //run the camera shake function
        StartCoroutine("CameraShake");

        //disbaled mesh
        mr.enabled = false;

        //wait 2 seconds
        yield return new WaitForSeconds(2);

        //destroy player
        Destroy(gameObject);

    }

    IEnumerator CameraShake()
    {
        isShaking = true;

        //initialize counter
        float t = shakeTime;

        //get camera initial position
        Vector3 ogPos = cam.position;

        while (t > 0)
        {
            //countdown with time
            t -= Time.deltaTime;

            //this will shake the camera based on its original position
            cam.position = ogPos + Random.insideUnitSphere * shakeAmount;

            yield return null;
        }

        //reset camera
        cam.position = ogPos;

        isShaking = false;
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
            //if not already dead
            if (!isDead)
                //run death code
                StartCoroutine("Death");
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
