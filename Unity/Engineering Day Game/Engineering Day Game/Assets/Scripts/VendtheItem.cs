using UnityEngine;

public class VendtheItem : StateMachineBehaviour
{
    Transform spawnPoint;
    Rigidbody2D rb;
    public GameObject[] vendingItems;
    SpriteRenderer sprite;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    player = GameObject.FindWithTag("Player").transform;
        spawnPoint = GameObject.Find("Vend Spawn").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();

        sprite.color = new Color(1, 1, 1, 1);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       int randItem = Random.Range(0, vendingItems.Length);
       Instantiate(vendingItems[randItem], spawnPoint.position, spawnPoint.rotation);
    }
}
