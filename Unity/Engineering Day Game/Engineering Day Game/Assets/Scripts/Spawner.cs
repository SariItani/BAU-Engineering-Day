using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] BEAN;
    int majorIndex;

    public float spawnRate;
    public bool disabled = false;
    public int maxSpawns;
    public float nextSpawn = 4f;
    private int spawns = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (disabled)
            maxSpawns = -1;
        majorIndex = PlayerPrefs.GetInt("Major Index");
        // BEAN.RemoveAt(majorIndex);
        // make the randEnemy range from [0, major index] [major index+1, BEAN.length]
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn && ((spawns <= maxSpawns) || (maxSpawns == 0)))
        {
            nextSpawn = Time.time + spawnRate;
            int randEnemy = Random.Range(0, BEAN.Length);
            while (randEnemy == majorIndex)
            {
                randEnemy = Random.Range(0, BEAN.Length);
            }
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(BEAN[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            spawns++;
        }
    }
}
