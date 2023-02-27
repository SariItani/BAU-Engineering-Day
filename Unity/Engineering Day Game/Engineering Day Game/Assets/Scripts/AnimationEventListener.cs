using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    GameObject player;

    AudioSource audiosource;
    public AudioClip punch;

    public int attackRadius = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audiosource = gameObject.GetComponent<AudioSource>();
    }
    public void EventListener(int damage)
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRadius)
        {
            audiosource.clip = punch;
            audiosource.Play();
            player.GetComponent<DamageableObject>().TakeDamage(damage);
        }
    }
}
