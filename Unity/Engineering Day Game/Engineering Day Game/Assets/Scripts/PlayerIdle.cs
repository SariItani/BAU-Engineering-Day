using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : StateMachineBehaviour
{

    // vars
    // Transform player;
    // public float speed = 0.21f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // player = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if (Input.GetAxis("Horizontal"))
        // {
        //     // player.Translate(Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime, 0, 0);
        //     animator.SetFloat("speed", speed);
        // }
        // else
        // {
        //     animator.SetFloat("speed", 0f);
        // }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
