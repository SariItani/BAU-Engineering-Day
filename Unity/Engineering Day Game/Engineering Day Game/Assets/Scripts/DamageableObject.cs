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
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
            currenthealth -= damage;
        if (currenthealth <= 0)
        {
            Die();
        }
        if (gameObject.tag == "Player")
        {
            healthBar.SetHealth(currentHealth);
            if (currentHealth == 0)
            {
                // Call the game over scene if the game object destroyed has the tag "Player"
                gameOver.EndGame();
                // Destroy(gameObject);
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}