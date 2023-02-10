using UnityEngine;

public class Chase : MonoBehaviour
{
    public float speed, distanceBetween;
    private float distance;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}

// float direction = (player.transform.position - transform.position).normalized.x;
// transform.LookAt(player.transform);