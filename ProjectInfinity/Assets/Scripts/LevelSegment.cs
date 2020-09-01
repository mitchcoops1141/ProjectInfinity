using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    public float speed;
    public float lifetime;

    void Start()
    {
        //destroy the gameobject after lifetime amount
        Destroy(gameObject, lifetime);
    }
    // Update is called once per frame
    void Update()
    {
        //move the level according to the speed
        transform.position = new Vector3(transform.position.x - speed * GameManager.instance.gameSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
