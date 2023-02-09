using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public int health = 100;
    public bool invincible = false;

    public void TakeDamage(int damage)
    {
        if (!invincible)
            health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}