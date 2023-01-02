using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private float x_vel = 0.0f;

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
        float ax = Input.GetAxis("Horizontal");
        if (!(Input.GetKeyDown(KeyCode.Space))){
            if( ax != 0.0f){
                if(ax > 0 ){
                    x_vel = 1;
                }
                else {
                    x_vel = -1;
                }
            }
        }
        else {
            x_vel = 0;
        }
        body.velocity = new Vector2(x_vel, body.velocity.y);
    }
}
