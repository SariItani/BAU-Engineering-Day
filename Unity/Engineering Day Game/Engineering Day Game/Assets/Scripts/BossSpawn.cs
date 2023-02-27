using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject boss;
    public float spawnRate;
    public bool disabled = false;
    public int maxSpawns;
    public float nextSpawn = 4f;
    private int spawns = 1;
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

            spawns++;
            nextSpawn = Time.time + spawnRate;

            Instantiate(boss, spawnPoint.position, transform.rotation);
        }
    }
}
