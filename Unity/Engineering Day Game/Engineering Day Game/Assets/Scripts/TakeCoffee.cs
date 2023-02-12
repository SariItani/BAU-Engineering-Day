using UnityEngine;

public class TakeCoffee : StateMachineBehaviour
{
    Transform player;
    Transform spawnPoint;
    Rigidbody2D rb;
    public GameObject mug;
    public float radius;
    SpriteRenderer sprite;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindWithTag("Player").transform;
       spawnPoint = GameObject.Find("Coffee Spawn").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       sprite = rb.GetComponent<SpriteRenderer>();
       Instantiate(mug, spawnPoint.position, spawnPoint.rotation);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, rb.position) <= radius)
        {
            sprite.color = new Color(1, 1, 1, 1);
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetTrigger("PlayerTakeCoffee");
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
       animator.ResetTrigger("PlayerTakeCoffee");
    }
}
