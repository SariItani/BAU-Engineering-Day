using UnityEngine;

public class ReflectBullet : MonoBehaviour
{

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Reflected Projectile");
        Rigidbody2D projectile = other.GetComponent<Rigidbody2D>();
        projectile.velocity = -projectile.velocity;

    }
}
