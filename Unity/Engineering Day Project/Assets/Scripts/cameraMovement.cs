using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
        if (Input.GetKeyDown(KeyCode.R))// refresh the current scence
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
