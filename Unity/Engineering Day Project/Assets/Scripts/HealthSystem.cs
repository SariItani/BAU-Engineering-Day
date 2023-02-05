using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int maxhealth = 10;
    void Start(){
        health = maxhealth;
    }

    public void TakeDamage(int amount){
        health = health - amount;
        if (health <= 0){
            Destroy(gameObject);
        }
    }
}