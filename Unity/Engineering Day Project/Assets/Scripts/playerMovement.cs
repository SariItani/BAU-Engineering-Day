using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    public bool facingRight = true;
    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public float jumpforce;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame

    // the direction of the player can only be set inside the actual class, but can be read from outside the class
    public float Direction { get { return facingRight ? 1 : -1; } }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

        var x_axis = Input.GetAxis("Horizontal");
        transform.position += new Vector3(x_axis * speed, 0);
        // greater than zero or very faint touch
        // Swap direction of sprite depending on move direction
        // If pressing right arrow key but not facing the right
        // or pressing left arrow key but facing right then flip
        if ((x_axis > 0 && !facingRight) || (x_axis < 0 && facingRight))
        {
            Flip();
        }
        if (isGrounded)
        {
            Debug.Log("Ground Detected.");
            // if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            // {
            //     Debug.Log("Pressed W or Space");
            //     rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            // }
        }
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
