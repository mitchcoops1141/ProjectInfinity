using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        //rotate the ball
        transform.Rotate(0, 0, -speed * Time.deltaTime);
    }
}
