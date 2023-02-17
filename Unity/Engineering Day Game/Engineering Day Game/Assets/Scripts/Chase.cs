using UnityEngine;

public class Chase : MonoBehaviour
{
    public float speed, distanceBetween;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var x_diff = (transform.position - player.transform.position).x;
        Vector3 angle_rotation = x_diff > 0 ? new(0, -180f, 0) : new(0, 0, 0);
        // BEAN is to the right of the player, look to the left ( BEAN is originally looking to the right)
        // so rotate by 180 degrees
        transform.eulerAngles = angle_rotation;
        if (x_diff <= distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
