using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int bullet_damage = 10;

    private Rigidbody2D rb;
    public float speed = 20;
    public LayerMask layer;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DamageableObject>() != null)
        {
            Debug.Log("Enemy hit");
            other.GetComponent<DamageableObject>().TakeDamage(bullet_damage);
            Destroy(gameObject);
        }
    }
}
