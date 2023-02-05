using UnityEngine;

public class DamagableObject : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public int GetHealth() => health;

    void Die()
    {
        Destroy(gameObject);
    }

}
