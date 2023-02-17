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
        // By default, BEAN looks to the right
        var x_diff = (transform.position - player.transform.position).x;
        // BEAN is to the right of the player, look the left. Look
        // to the right otherwise
        Vector3 angle_rotation = x_diff > 0 ? new Vector3(0, -180f, 0) : new(0, 0, 0);
        transform.eulerAngles = angle_rotation;
        // Bean is to the left of the player, look to the right
        if (x_diff <= distanceBetween)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
