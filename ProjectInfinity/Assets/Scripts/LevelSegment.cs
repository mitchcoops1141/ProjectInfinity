using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    public float speed;
    public float lifetime;

    bool canSpawn = true;

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

        //if the level is past the center position
        if (transform.position.x < 0)
        {
            //if it can spawn
            if (canSpawn)
                //if in test mode
                if (LevelManager.instance.testMode)
                {
                    //spawn the test level
                    SpawnLevel(LevelManager.instance.testLevel);
                }
                //if in normal mode
                else
                {
                    //spawn a random level
                    SpawnLevel(LevelManager.instance.levels[Random.Range(0, LevelManager.instance.levels.Length)]);
                }
        }
    }

    void SpawnLevel(GameObject nextSegment)
    {
        //set scanspawn to be false
        canSpawn = false;

        //variable for spawn position
        Vector3 nextSegmentSpawn = new Vector3(nextSegment.GetComponent<BoxCollider>().size.x - 0.1f, 0, 0);

        //create the level
        Instantiate(nextSegment, nextSegmentSpawn, nextSegment.transform.rotation);
    }
}
