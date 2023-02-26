using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float clampx0, clampx1, clampy0, clampy1;
    // -3.00f, 24.00f
    // 0.27f, 4.59f

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    void Update () 
    {
        transform.position = new Vector3 (
            Mathf.Clamp(player.position.x + offset.x, clampx0, clampx1),
            Mathf.Clamp(player.position.y + offset.y, clampy0, clampy1),
            offset.z);
    }
}
