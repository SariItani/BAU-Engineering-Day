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

    GameObject boss;

    void Start()
    {
        try
            {
                score = GameObject.Find("Score number").GetComponent<Score>();
            }
            catch
            {
                Debug.Log("There is no score..");
            }
        if (gameObject.tag == "Player")
        {
            healthBar = GameObject.Find("Health bar").GetComponent<HealthBar>();
            gameOver = GameObject.Find("Manager").GetComponent<GameOver>();
        }
        else if (gameObject.tag == "Boss")
        {
            healthBar = GameObject.Find("Health bar (Boss)").GetComponent<HealthBar>();
            gameOver = GameObject.Find("Manager").GetComponent<GameOver>();
        }
        currentHealth = maxhealth;
        if (gameObject.tag == "Player" || gameObject.tag == "Boss")
        {
            healthBar.SetMaxHealth(maxhealth);
        }
        boss = GameObject.FindGameObjectWithTag("Boss");
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
        if (gameObject.tag == "Player" || gameObject.tag == "Boss")
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
            if (gameObject == boss)
            {
                gameOver.WinGame(); // I was able to trace down the issue to this line over here, it doesn't get executed for some reason...
            }
        }
    }

    void Die()
    {
        try
        {
            score.ScorePoint(pointforkill);
        }
        catch
        {
            Debug.Log("There is no score");
        }
        Destroy(gameObject);
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

    // public void SetForPlayer()
    // {
    //     healthBar = GameObject.Find("Health bar").GetComponent<HealthBar>();
    //     gameOver = GameObject.Find("Manager").GetComponent<GameOver>();
    //     score = GameObject.Find("Score number").GetComponent<Score>();
    // }

}
