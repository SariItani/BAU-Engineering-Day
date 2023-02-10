using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] BEAN;
    private GameObject player;

    public float spawnRate = 4f;
    float nextSpawn = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            int randEnemy = Random.Range(0, BEAN.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(BEAN[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
        }
    }
}
