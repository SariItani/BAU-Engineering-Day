using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Stuff")]
    public float speed;
    private bool isGrounded = true;
    public Animator animator;
    public float groundDistance;
    public float jumpforce = 2.0f;
    public float Direction => facingRight == true ? 1 : -1;

    [Header("Required Objects and Delegates")]
    public GameObject bullet_prefab;
    public LayerMask entityLayer;
    public LayerMask groundMask;
    public System.Action AttackDelegate;
    private AudioSource audioData;
    public AudioClip hop, punch;

    [Header("Attack Properties")]
    public float bullet_cleanuptime = 2.0f;
    public float attack_radius = 2.5f;
    public int punch_damage = 10;
    public float x_offset = 2.5f;
    public bool canShoot = false;
    // private vars
    private Transform shoot_pos;
    private Transform groundCheck;
    private Vector3 pushed_vector;
    private Rigidbody2D rb;
    private bool facingRight = true;
    // the direction of the player can only be set inside the actual class, but can be read from outside the class

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioData = GetComponent<AudioSource>();
        AttackDelegate = Punch;
        shoot_pos = transform.Find("ShootPosition");
        groundCheck = transform.Find("GroundCheck");
        if (canShoot)
        {
            AttackDelegate = Shoot;
        }
        // animator.runtimeAnimatorController=Resources.Load<RuntimeAnimatorController>("Computer Engineer.controller");
        // full path: Assets/Recourses/ComputerEngineer/Computer Engineer.controller
        // thing i found on the form: animator.runtimeAnimatorController=Resources.Load<RuntimeAnimatorController>("Oinkers/PlayerPiggyOverride");
    }

    void FixedUpdate()
    {
        pushed_vector = shoot_pos.position + Utils.ToVector3(Direction * x_offset);

        if (canShoot)
        {
            AttackDelegate = Shoot;
        }
        else
        {
            AttackDelegate = Punch;
        }

        transform.position += new Vector3(speed, 0);

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackDelegate();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);
        var x_axis = Input.GetAxis("Horizontal");
        if (isGrounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            audioData.clip = hop;
            audioData.Play();
        }

        // Player speed determination
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackDelegate();
        }
        if (Mathf.Abs(x_axis) > 0)

        {
            speed = Mathf.Sign(x_axis) * 0.15f;
        }
        else
        {
            speed = 0;
        }
        if (x_axis < 0 && facingRight || x_axis > 0 && !facingRight)
        {
            Flip();
        }
        animator.SetFloat("Speed", Mathf.Abs(speed));
        animator.SetBool("OnGround", isGrounded);
    }

    void Flip()
    {

        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
    void Shoot()
    {
        // make sure to offset correctly depending on direction 
        animator.SetTrigger("throwTrigger");
        animator.ResetTrigger("attackTrigger");
        canShoot = false;
        var bullet_obj = Instantiate(bullet_prefab, pushed_vector, shoot_pos.rotation);
        // clean up the bullet if it goes out of bounds
        Destroy(bullet_obj, bullet_cleanuptime);
    }
    void Punch()
    {
        if (Mathf.Abs(speed) > 0.1f)
        {
            animator.SetTrigger("walkattackTrigger");
        }
        else
        {
            animator.SetTrigger("attackTrigger");
        }
        animator.ResetTrigger("throwTrigger");
        Collider2D enemy = Physics2D.OverlapCircle(pushed_vector, attack_radius);
        audioData.clip = punch;
        audioData.Play();

        DamageableObject.DamageObject(enemy, punch_damage);
    }

    public void SetShoot()
    {
        canShoot = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pushed_vector, attack_radius);
    }
}
