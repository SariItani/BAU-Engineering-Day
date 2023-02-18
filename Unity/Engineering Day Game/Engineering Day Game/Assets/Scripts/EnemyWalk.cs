using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : StateMachineBehaviour
{
    GameObject player;
    Transform enemy;
    public float attackRange;
    float distance;
    public bool isFlipped = false;
    Animator animator;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Player");
        enemy = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       distance = (enemy.position - player.transform.position).x;
       animator.SetFloat("distance", Mathf.Abs(distance));
       //Debug.Log("Distance : " + distance);
// flipping
// shahbaz idk what magic you do but plz flip the fucking dude
        Vector3 flipped = enemy.localScale;
        flipped.z *= -1f;
        if (enemy.position.x > player.transform.position.x && isFlipped)
        {
            enemy.localScale = flipped;
            // enemy.rotation = (0f, 180f, 0f);
            enemy.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        if (enemy.position.x < player.transform.position.x && !isFlipped)
        {
            enemy.localScale = flipped;
            // enemy.rotation = (0f, 180f, 0f);
            enemy.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
// transition to attack
        if (Vector2.Distance(player.transform.position, enemy.position) <= attackRange)
        {
            animator.SetTrigger("attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }
}
