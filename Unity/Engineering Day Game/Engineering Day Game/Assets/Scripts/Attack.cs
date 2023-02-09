using System;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [Header("Required Objects and Delegates")]
    public GameObject bullet_prefab;
    public LayerMask entityLayer;

    public Action AttackDelegate;
    [Header("Attack Properties")]
    public float bullet_cleanuptime = 2.0f;
    public float attack_radius = 1.0f;
    public int punch_damage = 10;
    public float x_offset = 0.05f;
    public bool canShoot = false;
    private Transform shoot_pos;
    private Vector3 pushed_vector;

    void Awake()
    {
        // common position for melee and projectiles
        AttackDelegate = Punch;
        shoot_pos = transform.Find("ShootPosition");
        if (canShoot)
        {
            AttackDelegate += Shoot;
        }
    }

    void Update()
    {

        pushed_vector = shoot_pos.position + Utils.ToVector3(transform.GetComponent<playerMovement>().Direction * x_offset);
        if (Input.GetKeyDown(KeyCode.F))
        {
            AttackDelegate();
        }

    }
    public void Shoot()
    {
        // make sure to offset correctly depending on direction 
        var bullet_obj = Instantiate(bullet_prefab, pushed_vector, shoot_pos.rotation);
        // var bullet_obj = Instantiate(bullet_prefab, Utils.ToVector3(pushed_vector), shoot_pos.rotation);
        // clean up the bullet if it goes out of bounds
        Destroy(bullet_obj, bullet_cleanuptime);
    }
    public void Punch()
    {
        Collider2D enemy = Physics2D.OverlapCircle(pushed_vector, attack_radius, entityLayer);
        try
        {
            enemy.GetComponent<DamageableObject>().TakeDamage(punch_damage);
            Debug.Log("Enemy hit");
        }
        catch (NullReferenceException)
        {
            // shut the fuck up unity I KNOW THERE IS NO OBJECT
        }
    }


}
