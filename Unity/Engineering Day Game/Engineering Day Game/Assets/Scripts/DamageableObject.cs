using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public int maxhealth = 100;
    public int currentHealth;
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

    // public void LateUpdate()
    // {
    //     transform.Rotate(0f, 180f, 0f);
    //     facingRight = !facingRight;
    // }

    void Die()
    {
        Destroy(gameObject);
        if (gameObject.tag == "Enemy")
        {
            score.ScorePoint();
        }
    }

    public static void DamageObject(Component obj, int damage)
    {
        try
        {
            obj.GetComponent<DamageableObject>().TakeDamage(damage);
            Debug.Log("Enemy hit");
        }
        catch (System.NullReferenceException)
        {
            // shut the fuck up unity I KNOW THERE IS NO OBJECT
        }
    }

}