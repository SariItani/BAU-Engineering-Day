using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] objectPrefabs;
    // public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.Find("Player");
        // Debug.Log("Spotted the player : ");
        // Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            int randObject = Random.Range(0, objectPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(objectPrefabs[randObject], spawnPoints[randSpawnPoint].position, transform.rotation);
        }
    }
}
