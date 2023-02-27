using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : StateMachineBehaviour
{
    GameObject player;
    Transform enemy;
    float distance;
    public bool isFlipped = false;
    public bool isGrounded;
    public Chase chase;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = animator.GetComponent<Transform>();
        chase = animator.GetComponent<Chase>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isGrounded = chase.IsGrounded();
        animator.SetBool("OnGround", isGrounded);
        distance = (enemy.position - player.transform.position).x;
        animator.SetFloat("distance", Mathf.Abs(distance));
        //Debug.Log("Distance : " + distance);
        Vector3 flipped = enemy.localScale;
        flipped.z *= -1f;
        if (enemy.position.x > player.transform.position.x && isFlipped)
        {
            // enemy.localScale = flipped;
            // enemy.rotation = (0f, 180f, 0f);
            enemy.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        if (enemy.position.x < player.transform.position.x && !isFlipped)
        {
            // enemy.localScale = flipped;
            // enemy.rotation = (0f, 180f, 0f);
            enemy.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}