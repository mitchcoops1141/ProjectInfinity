using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("TESTING")]
    public bool testMode;
    public GameObject testLevel;

    [Header("LEVELS")]
    public GameObject[] levels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NextLevelSpawner"))
        {
            if (testMode)
            {
                spawnLevel(testLevel);
            }
            else
            {
                spawnLevel(levels[Random.Range(0, levels.Length)]);
            }
        }

        void spawnLevel(GameObject nextSegment)
        {
            //variable for spawn position
            Vector3 nextSegmentSpawn = new Vector3(nextSegment.GetComponent<BoxCollider>().size.x - 1f, 0, 0);

            //create the level
            Instantiate(nextSegment, nextSegmentSpawn, nextSegment.transform.rotation);
        }
    }
}
