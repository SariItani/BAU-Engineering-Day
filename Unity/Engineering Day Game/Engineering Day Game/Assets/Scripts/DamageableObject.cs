using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public int maxhealth = 100;
    public int currentHealth;
    public bool invincible = false;
    // player related
    protected bool facingRight = true;

    void Start()
    {
        currentHealth = maxhealth;
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
            currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
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