using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<DamagableObject>().TakeDamage(10);
        Destroy(gameObject);
    }
}
