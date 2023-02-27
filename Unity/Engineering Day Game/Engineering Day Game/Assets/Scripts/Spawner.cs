using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Random = System.Random;

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
    Random random = new Random();
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
            // create array from 0 to majorIndex (non-inclusive)
            int[] arr1 = Enumerable.Range(0, majorIndex).ToArray();
            // create array from majorIndex + 1 to BEAN.length(basically 7)
            int[] arr2 = Enumerable.Range(majorIndex + 1, BEAN.Length).ToArray();
            // concat the two arrays and pick a randEnemy from the result
            int[] range1 = arr1.Concat(arr2).ToArray();
            int randEnemy = range1[random.Next(range1.Length)]; // Random from the array...
            // we can select a random index, since range is an int[], by Random.Range(0, range.length), then we pass this selected index i to the range int[i] and this is our randEnemy
            int[] range2 = Enumerable.Range(0, spawnPoints.Length).ToArray();
            int randSpawnPoint = range2[random.Next(range2.Length)];

            Instantiate(BEAN[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            spawns++;
        }
    }
}
