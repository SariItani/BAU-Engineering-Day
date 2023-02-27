using UnityEngine;

public class Chase : MonoBehaviour
{
    public float speed, distanceBetween;
    private GameObject player;
    bool isGrounded;
    Transform groundCheck;
    public float groundDistance = 0.1f, jumpforce = 20.0f;
    public LayerMask groundMask;
    AudioSource audioData;
    public AudioClip hop;
    Rigidbody2D rb;
    // 
    // NOTE
    // 
    // PLACE GROUND CHECKS AND INITIALIZE AUDIO SOURCE CLIPS
    // 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        groundCheck = transform.Find("GroundCheck");
        audioData = gameObject.GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var x_diff = (transform.position - player.transform.position).x;
        Vector3 angle_rotation = x_diff > 0 ? new(0, -180f, 0) : new(0, 0, 0);
        // BEAN is to the right of the player, look to the left ( BEAN is originally looking to the right)
        // so rotate by 180 degrees
        transform.eulerAngles = angle_rotation;
        if (x_diff <= distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Fixed Object" && isGrounded)
        {
            // jump
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            audioData.clip = hop;
            audioData.Play();
        }
    }
}
