using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    public float speed;
    public float topLimit;
    public float bottomLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the light is too high
        if (transform.position.y > topLimit)
        {
            //make the pseed opposite
            speed = speed * -1;
        }
        //if the light is too low
        if (transform.position.y < bottomLimit)
        {
            //make the speed opposite
            speed = speed * -1;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + speed * GameManager.instance.gameSpeed * Time.deltaTime, transform.position.z);
      
        
    }
}
