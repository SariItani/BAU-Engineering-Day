using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxhealth = 20;
    public int damage = 4;
    public int currentHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.J))
    //     {
    //         takeDamage(damage);
    //     }
    // }

    public void takeDamage(int damage)
    {
        if (currentHealth >= damage)
        { 
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth = 0;
            // Call the game over scene if the game object destroyed has the tag "Player"
        }
    }
}
