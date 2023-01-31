﻿using UnityEngine;

public class PrototypeHeroDemo : DamagableObject
{

    [Header("Variables")]
    [SerializeField] float m_maxSpeed = 4.5f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] bool m_hideSword = false;
    [SerializeField] GameObject bullet_prefab;
    [SerializeField] int x_offset = 2;
    [SerializeField] float bullet_delay = 2.0f;
    [Header("Effects")]
    [SerializeField] GameObject m_RunStopDust;
    [SerializeField] GameObject m_JumpDust;
    [SerializeField] GameObject m_LandingDust;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Prototype m_groundSensor;
    private AudioSource m_audioSource;
    private AudioManager_PrototypeHero m_audioManager;
    private bool m_grounded = false;
    private bool m_moving = false;
    private int m_facingDirection = 1;
    private float m_disableMovementTimer = 0.0f;
    private bool m_facingRight = true;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_PrototypeHero.instance;
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease timer that disables input movement. Used when attacking
        m_disableMovementTimer -= Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = 0.0f;

        if (m_disableMovementTimer < 0.0f)
            inputX = Input.GetAxis("Horizontal");

        // GetAxisRaw returns either -1, 0 or 1
        float inputRaw = Input.GetAxisRaw("Horizontal");
        // Check if current move input is larger than 0 and the move direction is equal to the characters facing direction
        if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
            m_moving = true;

        else
            m_moving = false;

        // Swap direction of sprite depending on move direction
        // If pressing right arrow key but not facing the right
        // or pressing left arrow key but facing right then flip
        if ((inputX > 0 && !m_facingRight) || (inputX < 0 && m_facingRight))
        {
            Flip();
            m_facingDirection = (int)Mathf.Sign(inputRaw);
        }
        // sets the facing direction to be -1 or 1 depending on the
        // direction of the player. -1 is left, 1 is right, and 0 is not moving.
        // SlowDownSpeed helps decelerate the characters when stopping
        float SlowDownSpeed = m_moving ? 1.0f : 0.0f;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Transform shoot_pos = transform.Find("ShootPosition");
            // make sure to offset correctly depending on direction 
            var bullet_obj = Instantiate(bullet_prefab, shoot_pos.position + new Vector3(m_facingDirection * x_offset, 0f, 0f), shoot_pos.rotation);
            // clean up the bullet if it goes out of bounds
            Destroy(bullet_obj, bullet_delay);
        }
        // Set movement
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_body2d.velocity = new Vector2(Mathf.Sign(inputX) * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);
        }
        else
        {
            m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);
        }

        // Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // Set Animation layer for hiding sword
        int boolInt = m_hideSword ? 1 : 0;
        m_animator.SetLayerWeight(1, boolInt);

        // -- Handle Animations --
        //Jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && m_grounded && m_disableMovementTimer < 0.0f)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (m_moving)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    // Function used to spawn a dust effect
    // All dust effects spawns on the floor
    // dustXoffset controls how far from the player the effects spawns.
    // Default dustXoffset is zero
    void SpawnDustEffect(GameObject dust, float dustXOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, 0.0f, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
        }
    }

    // Animation Events
    // These functions are called inside the animation files
    void AE_runStop()
    {
        m_audioManager.PlaySound("RunStop");
        // Spawn Dust
        float dustXOffset = 0.6f;
        SpawnDustEffect(m_RunStopDust, dustXOffset);
    }

    void AE_footstep()
    {
        m_audioManager.PlaySound("Footstep");
    }

    void AE_Jump()
    {
        m_audioManager.PlaySound("Jump");
        // Spawn Dust
        SpawnDustEffect(m_JumpDust);
    }

    void AE_Landing()
    {
        m_audioManager.PlaySound("Landing");
        // Spawn Dust
        SpawnDustEffect(m_LandingDust);
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        m_facingRight = !m_facingRight;
    }

}
