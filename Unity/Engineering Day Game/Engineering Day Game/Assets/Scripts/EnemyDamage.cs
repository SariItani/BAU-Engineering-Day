using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    private DamageableObject damageableObject;

    void Start()
    {
        damageableObject = GameObject.Find("Player").GetComponent<DamageableObject>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.transform.GetComponent<DamageableObject>();
            player.TakeDamage(damage);
        }
    }
}
