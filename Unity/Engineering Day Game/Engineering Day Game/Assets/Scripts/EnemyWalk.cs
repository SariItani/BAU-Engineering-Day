using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : StateMachineBehaviour
{
    GameObject player;
    GameObject boss;
    public float attackRange;
    float distance;
    public bool isFlipped = true;
    Animator animator;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        boss = GameObject.FindWithTag("Boss");
        animator = boss.GetComponent<Animator>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       distance = (boss.transform.position - player.transform.position).x;
       animator.SetFloat("distance", Math.abs(distance));
       Debug.Log("Distance : " + distance);
// flipping
// shahbaz idk what magic you do but plz flip the fucking dude
        Vector3 flipped = boss.transform.localScale;
        flipped.z *= -1f;
        if (boss.transform.position.x > player.transform.position.x && isFlipped)
        {
            // boss.transform.localScale = flipped;
            // boss.transform.rotation = (0f, 180f, 0f);
            boss.transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        if (boss.transform.position.x < player.transform.position.x && !isFlipped)
        {
            // boss.transform.localScale = flipped;
            // boss.transform.rotation = (0f, 180f, 0f);
            boss.transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
// transition to attack
        if (Vector2.Distance(player.transform.position, boss.transform.position) <= attackRange)
        {
            animator.SetTrigger("attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }
}
