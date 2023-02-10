using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public int maxhealth = 100;
    public int currentHealth;
    public bool invincible = false;
    // player related
    public GameOver gameOver;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxhealth;
        if (gameObject.tag == "Player")
        {
            healthBar.SetMaxHealth(maxhealth);
        }
    }

    public void Heal(int heal)
    {
        if (currentHealth + heal > maxhealth)
        {
            currentHealth = maxhealth;
        }
        else
        {
            currentHealth += heal;
        }
        if (gameObject.tag == "Player")
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
            currentHealth -= damage;
        if (gameObject.tag == "Player")
        {
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
            if (gameObject.tag == "Player")
            {
                gameOver.EndGame();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}