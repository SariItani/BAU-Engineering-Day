using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    private Animator animator;
    private bool isMakingCoffee = false;

    void Start()
    {
        animator = gameObject.Find("Coffee Machine").GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isMakingCoffee)
        {
            animator.SetTrigger("PlayerWantsCoffee");
        }
        // else if (collision.gameObject.tag == "Player" && !isMakingCoffee)
        // {

        // }
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
