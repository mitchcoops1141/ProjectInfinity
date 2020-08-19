using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float speed;
    public GameObject[] levelSegments;

    bool shouldSpawnSegment = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (shouldSpawnSegment)
        {
            StartCoroutine("SpawnSegment");
        }
    }

    IEnumerator SpawnSegment()
    {
        shouldSpawnSegment = false;

        int nextSegmentValue = Random.Range(0, levelSegments.Length);
        GameObject nextSegment = levelSegments[nextSegmentValue];

        Instantiate(nextSegment, new Vector3(25, transform.position.y, transform.position.z), transform.rotation);

        yield return new WaitForSeconds(4.98f);

        shouldSpawnSegment = true;
    }
}
