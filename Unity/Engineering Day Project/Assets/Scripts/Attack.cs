using UnityEngine;

public class Attack : MonoBehaviour
{

    public float speed = 20;
    public GameObject bullet_prefab;
    public int x_offset = 2;
    public LayerMask entityLayer;
    [Header("Attack Properties")]
    public float bullet_cleanuptime = 2.0f;
    public float attack_radius = 2.0f;
    public int punch_damage = 10;

    private Rigidbody2D rb;
    void Start()
    {
        rb = bullet_prefab.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {

        // common position for melee and projectiles
        Transform shoot_pos = transform.Find("ShootPosition");
        if (Input.GetKeyDown(KeyCode.F))
        {
            // make sure to offset correctly depending on direction 
            var bullet_obj = Instantiate(bullet_prefab, shoot_pos.position + new Vector3(transform.parent.GetComponent<playerMovement>().Direction * x_offset, 0f, 0f), shoot_pos.rotation);
            // clean up the bullet if it goes out of bounds
            Destroy(bullet_obj, bullet_cleanuptime);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(Utils.ToVector2(shoot_pos.position), attack_radius, entityLayer);
            if (Utils.ArrayNotEmpty<Collider2D>(enemies))
            {
                enemies[0].GetComponent<DamagableObject>().TakeDamage(punch_damage);
            }

        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attack_radius);
    }
}
