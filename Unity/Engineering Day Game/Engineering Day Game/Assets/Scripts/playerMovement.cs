using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    bool isGrounded, isAttacking, throwing;
    public Animator animator;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public float jumpforce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    public float Direction => facingRight == true ? 1 : -1;

    // the direction of the player can only be set inside the actual class, but can be read from outside the class

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Debug.Log(rb);
    }

    void Update()
    {
        animator.SetFloat("Speed", speed);
        // animator.SetBool("isAttacking", isAttacking);
        // animator.SetBool("Throwing", throwing);
        animator.SetBool("OnGround", isGrounded);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, 0);

        if(isGrounded)
        {
            // Debug.Log("Ground Detected.");
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                // Debug.Log("Pressed W or Space");
                // animator.SetTrigger("jumpTrigger");
                rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            }
        }
        // if(Input.GetKeyDown(KeyCode.T))
        // {
        //     throwing = true;
        // }
        // else
        // {
        //     throwing = false;
        // }

        // Player speed determination
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            speed = 0.15f;
        }
        else
        {
            speed = 0.0f;
        }

    //     if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.J))
    //     {
    //         isAttacking = true;
    //     }
    //     else
    //     {
    //         isAttacking = false;
    //     }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    void Flip()
    {

        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
}
