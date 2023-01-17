using UnityEngine;

public class ReflectBullet : MonoBehaviour
{

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hit)
    {
        Debug.Log("Reflected Projectile");
        Rigidbody2D projectile = hit.GetComponent<Rigidbody2D>();
        projectile.velocity = -projectile.velocity;

    }
}
