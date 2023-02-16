using UnityEngine;

public class WantCoffee : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float radius;
    SpriteRenderer sprite;
    AudioSource audioData;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        audioData = rb.GetComponent<AudioSource>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, rb.position) <= radius)
        {
            sprite.color = new Color(1, 1, 1, 1);
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetTrigger("PlayerWantsCoffee");
            }
        }
        else
        {
            sprite.color = new Color(1, 1, 1, 0.9f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("PlayerWantsCoffee");
        audioData.Play(0);
    }
}
