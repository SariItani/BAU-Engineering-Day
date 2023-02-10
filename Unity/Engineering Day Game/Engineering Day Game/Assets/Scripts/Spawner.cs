using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject BEAN;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(BEAN, spawnPoints[randSpawnPoint].position, transform.rotation);
        }
    }
}
