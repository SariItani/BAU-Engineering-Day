using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public GameObject player;
    public DamageableObject playerHealth;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<DamageableObject>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            playerHealth.Heal(health);
            Destroy(gameObject);
        }
    }
}
