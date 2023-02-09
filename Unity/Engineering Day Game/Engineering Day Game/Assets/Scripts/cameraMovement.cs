using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    
    void Update () 
    {
        transform.position = new Vector3 (
            Mathf.Clamp(player.position.x + offset.x, -3.50f, 24.50f),
            Mathf.Clamp(player.position.y + offset.y, 0.27f, 4.59f),
            offset.z);
        // Camera follows the player with specified offset position
    }
}
