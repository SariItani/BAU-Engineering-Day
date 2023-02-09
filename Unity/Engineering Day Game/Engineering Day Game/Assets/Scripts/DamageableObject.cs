using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public int maxhealth = 100;
    public int currentHealth;
    public bool invincible = false;

    void Start()
    {
        currentHealth = maxhealth;
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
            currenthealth -= damage;
        if (currenthealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}