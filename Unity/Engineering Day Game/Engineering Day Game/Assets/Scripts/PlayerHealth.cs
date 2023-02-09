using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public DamageableObject damageableObject;
    // public GameOver gameOver;
    public HealthBar healthBar;

    void Update()
    {
        healthBar.SetHealth(damageableObject.currentHealth);
        // if (damageableObject.currentHealth == 0)
        // {
        //     // Call the game over scene if the game object destroyed has the tag "Player"
        //     gameOver.EndGame();
        //     // Destroy(gameObject);
        // }
    }
}