using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    public void EventListener(int damage)
    {
        player.GetComponent<DamageableObject>().TakeDamage(damage);
    }
}
