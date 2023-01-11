using UnityEngine;

public class PlayerMovement : BoundedObject
{
    public float speed_modifier = 1.5f;
    private Rigidbody2D body;
    private float x_vel = 0.0f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // toggle the direction of the velocity 
        // simply makes sure x_vel and y_vel are either 0, -1, or 1.
        // -1 is left and 1 is right.
        float x_axis = Input.GetAxis("Horizontal");
        x_vel = Sign(x_axis);
        // normalize vector to make diagonal motion natural
        body.velocity = new Vector2(x_vel, body.velocity.y).normalized * speed_modifier;
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
