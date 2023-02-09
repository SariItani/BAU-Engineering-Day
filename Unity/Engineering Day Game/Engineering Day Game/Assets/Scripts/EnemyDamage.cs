using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public DamageableObject damageableObject;
    public int damage;

    void Start()
    {
        damageableObject = GameObject.Find("Player").GetComponent<DamageableObject>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            damageableObject.TakeDamage(damage);
        }
    }
}
