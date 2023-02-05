using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    bool isGrounded;
    public Transform groundCheck;
    public Animation animation;
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, 0);

        if(isGrounded)
        {
            Debug.Log("Ground Detected.");
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Pressed W or Space");
                rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
