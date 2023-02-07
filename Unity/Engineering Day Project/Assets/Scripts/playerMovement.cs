using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    bool isGrounded, isAttacking;
    public Animator animator;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public float jumpforce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    public float Direction => facingRight == true ? 1 : -1;

    // the direction of the player can only be set inside the actual class, but can be read from outside the class
    void Update()
    {
        // animator.SetFloat("speed", speed);
        // animator.SetBool("attacking", isAttacking);
        // animator.SetInt("combo", combo);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);
        var x_dir = Input.GetAxis("Horizontal");
        transform.position += new Vector3(x_dir * speed, 0);

        if (isGrounded)
        {
            Debug.Log("Ground Detected.");
            // if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            // {
            //     Debug.Log("Pressed W or Space");
            //     rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            // }
        }
        // Player speed determination
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.21f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            speed = 0.1f;
        }
        else
        {
            speed = 0.0f;
        }
        if (x_dir < 0 && facingRight || x_dir > 0 && !facingRight)
            Flip();
    }

    void Flip()
    {

        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
}
