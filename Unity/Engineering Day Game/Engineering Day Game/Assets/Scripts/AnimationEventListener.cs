using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    GameObject player;

    AudioSource audiosource;
    public AudioClip punch;
    public float multiplier;

    public int attackRadius = 2;

    void Start()
    {
        if (gameObject.tag == "Boss")
        {
            multiplier = 3.0f;
        }
        else
        {
            multiplier = 1.0f;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        audiosource = gameObject.GetComponent<AudioSource>();
    }
    public void EventListener(int damage)
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRadius)
        {
            audiosource.clip = punch;
            audiosource.Play();
            player.GetComponent<DamageableObject>().TakeDamage((int)(damage * multiplier));
        }
    }
}
