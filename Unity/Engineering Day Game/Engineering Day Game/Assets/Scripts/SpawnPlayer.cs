using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // string major = PlayerPrefs.GetString("Major", "Computer");
    int majorIndex;
    public GameObject[] players;
    public Transform spawnPoint;
    public cameraMovement cameraMovement;

    // Start is called before the first frame update
    void Start()
    {
        cameraMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraMovement>();
        majorIndex = PlayerPrefs.GetInt("MajorIndex");
        // Computer 0
        // yada yada yada
        Debug.Log("Activated Game... Loading major index: " + majorIndex);
        Instantiate(players[majorIndex], spawnPoint.position, transform.rotation);
        cameraMovement.SetPlayer();
    }

}
