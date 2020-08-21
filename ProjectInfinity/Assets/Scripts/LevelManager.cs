using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("START BOTTOM LEVELS")]
    public GameObject[] Blevels;
    [Header("START TOP LEVELS")]
    public GameObject[] Tlevels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        //if the next level should start at the top then spawn a level through the function with a random level from the TOP array
        if (other.CompareTag("NextStartTop")) spawnLevel(Tlevels[Random.Range(0, Tlevels.Length)]);

        //if the next level should start at the top then spawn a level through the function with a random level from the BOTTOM array
        if (other.CompareTag("NextStartBottom")) spawnLevel(Blevels[Random.Range(0, Blevels.Length)]);

        void spawnLevel(GameObject nextSegment)
        {
            //variable for spawn position
            Vector3 nextSegmentSpawn = new Vector3(nextSegment.GetComponent<BoxCollider>().size.x - 0.25f, 0, 0);

            //create the level
            Instantiate(nextSegment, nextSegmentSpawn, nextSegment.transform.rotation);
        }
    }
}
