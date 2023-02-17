using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    GameObject player;

    public int attackRadius = 2;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    public void EventListener(int damage)
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRadius)
        {
            player.GetComponent<DamageableObject>().TakeDamage(damage);
        }
    }
}
