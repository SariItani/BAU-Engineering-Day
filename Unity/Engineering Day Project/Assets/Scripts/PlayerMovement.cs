using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private float x_vel = 0.0f;
    private float y_vel = 0.0f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        // Start is called before the first frame update
    }

    // Update is called once per frame
    void Update()
    {
        // toggle the direction of the velocity 
        // simply makes sure x_vel and y_vel are either 0, -1, or 1.
        // -1 is left and 1 is right.
        float x_axis = Input.GetAxis("Horizontal");
        float y_axis = Input.GetAxis("Vertical");
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            x_vel = Sign(x_axis);
            y_vel = Sign(y_axis);
        }
        else
        {
            x_vel = 0;
            y_vel = 0;
        }
        // normalize vector to make diagonal motion natural
        body.velocity = new Vector2(x_vel, y_vel).normalized;
    }

    int Sign(float num)
    {
        if (num > 0)
        {
            return 1;
        }
        else if (num == 0)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }


}
