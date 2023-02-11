using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] BEAN;

    public float spawnRate;
    public bool disabled = false;
    public int maxSpawns;
    float nextSpawn = 0f;
    private int spawns = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (disabled)
            maxSpawns = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn && ((spawns <= maxSpawns) || (maxSpawns == 0)))
        {
            nextSpawn = Time.time + spawnRate;
            int randEnemy = Random.Range(0, BEAN.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(BEAN[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            spawns++;
        }
    }
}
