using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    public Animator animator;
    public Vector3 scaleChange;
    bool isGrounded, isAttacking;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public float jumpforce;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb);
    }


    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", speed);
        animator.SetBool("attacking", isAttacking);
        // animator.SetInt("combo", combo);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

        if (Input.GetAxisRaw("Horizontal") == -1f)
        {
            scaleChange = new Vector3(1f, 1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1f)
        {
            scaleChange = new Vector3(-1f, 1f, 1f);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if(isGrounded)
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
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            speed = 0.1f;
        }
        else
        {
            speed = 0.0f;
        }
// Player speed execution
        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, 0);
        transform.localScale = scaleChange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
