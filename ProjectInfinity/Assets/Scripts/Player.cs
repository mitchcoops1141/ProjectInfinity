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

    [Header("RAY")]
    public LayerMask floor;
    public LayerMask level;
    public float rayDistance;

    //private varibles
    bool onContactWithFloor = false;

    bool isShaking = false;
    bool isDead = false;

    Rigidbody rb;
    Vector3 movement = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //starting variable
        isDead = false;

        //get rigidbody
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        //call function
        LandedRay();

        //set velocity to movement vector
        rb.velocity = movement;
    }

    void Update()
    {

        //if the player presses space bar or touches the screen and the player is on the floor or the roof, and the game is not paused
        if ((Input.GetButtonDown("Jump") || Input.touchCount > 0) && onContactWithFloor && Time.timeScale == 1f)
        {
            //swap direction
            direction = -direction;
            //play the jump sound
            AudioManager.instance.PlayOneShot("Jump");
        }

        //lock x position in place
        if (transform.position.x > 0)
        {
            //restict veloctiy
            movement.x = 0;
            //restrict position
            transform.position = new Vector3(0, transform.position.y, 0);
        }
    }

    IEnumerator Death()
    {
        isDead = true;

        //play the death sound
        AudioManager.instance.PlayOneShot("Death");
        AudioManager.instance.Stop("Song");

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

    void LandedRay()
    {
        //variable for the speed
        float s = speed * GameManager.instance.gameSpeed;

        //create a ray
        Ray ray = new Ray(transform.position, transform.up * direction);

        //store the hit of the Ray
        RaycastHit hit;

        //if it hits soemthing that is a floor or a level
        if (Physics.Raycast(ray, out hit, rayDistance, floor) || Physics.Raycast(ray, out hit, rayDistance, level))
        {
            //draw green line if hitting something
            Debug.DrawRay(transform.position, transform.up * rayDistance * direction, Color.green);

            //if the ray hits a floor
            if (hit.transform.CompareTag("Floor"))
            {
                //set bool to be true
                onContactWithFloor = true;
            }

            //if the players position is less then the center
            if (transform.position.x < 0)
            {
                //increase horizontal movement to get back to center
                movement.x = s / 5;
            } 
        }
        //if it doesnt hit something interactable
        else
        {
            //draw red line if not hitting something
            Debug.DrawRay(transform.position, transform.up * rayDistance * direction, Color.red);

            //set bool to false
            onContactWithFloor = false;

            //increase movement for y velocty
            movement.y = direction * s; 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            //if not already dead
            if (!isDead)
                //run death code
                StartCoroutine("Death");
        }
    }
}
