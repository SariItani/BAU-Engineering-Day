using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            Debug.Log("randEnemy: " + randEnemy);
            while (randEnemy == majorIndex)
            {
                randEnemy = Random.Range(0, BEAN.Length);
                Debug.Log("randEnemy: " + randEnemy);
            }
            // int[] numbers = { 1, 3, 4, 9, 2 };
            // int numToRemove = 4;
            // numbers = numbers.Where(val => val != numToRemove).ToArray();
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(BEAN[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            spawns++;
        }
    }
}
