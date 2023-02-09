using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject player;
    public float speed, distanceBetween;
    private float distance;
    
    // Update is called once per frame
    void Start()
    {
        player = GameObject.Find("Player");
    }
    
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
// Shahbaz, i need your script for rotating the player to apply it here instead of the angle script that i found in one of the online turorials, but generally, the rotation should go here.
// if i recall correctly, then you will have a sign function that returns -1, 0, or 1, and some sort of method for determining if the object is facing left or right, and then what would happen is that the object's transform would rotate 180f degrees into the direction desired.
        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
