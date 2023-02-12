using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class DamageableObject : MonoBehaviour
{
    public int maxhealth = 100;
    public int currentHealth;
    public int pointforkill;
    public bool invincible = false;
    // player related
    public GameOver gameOver;
    public HealthBar healthBar;
    public Score score;

    void Start()
    {
        score = GameObject.Find("Score number").GetComponent<Score>();
        currentHealth = maxhealth;
        if (gameObject.tag == "Player")
        {
            healthBar.SetMaxHealth(maxhealth);
        }
        if (gameObject.tag == "Boss")
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
        if (gameObject.tag == "Boss")
        {
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
            if (gameObject.tag == "Player")
            {
                if (Convert.ToInt32(score.ShowText()) >= Convert.ToInt32(PlayerPrefs.GetString("Highscore", "0")))
                {
                    PlayerPrefs.SetString("Highscore", score.ShowText());
                }
                gameOver.EndGame();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
        if (gameObject.tag == "Enemy")
        {
            score.ScorePoint(pointforkill);
        }
        if (gameObject.tag == "Boss")
        {
            score.ScorePoint(pointforkill);
            SceneManager.LoadScene(5);
        }
    }

    public static void DamageObject(Component obj, int damage)
    {
        try
        {
            obj.GetComponent<DamageableObject>().TakeDamage(damage);
        }
        catch (System.NullReferenceException)
        {
            // shut the fuck up unity I KNOW THERE IS NO OBJECT
        }
    }
}
