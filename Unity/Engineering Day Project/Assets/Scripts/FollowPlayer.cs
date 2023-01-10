using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;
    public float x_min;
    public float x_max;
    public float y_min;
    public float y_max;
    void FixedUpdate()
    {
        float final_x = x_max != 0 && x_min != 0 ? Mathf.Clamp(target.position.x, x_min, x_max) : target.position.x;
        float final_y = y_max != 0 && y_min != 0 ? Mathf.Clamp(target.position.y, y_min, y_max) : target.position.y;
        transform.position = new Vector2(
           final_x,
           final_y
        );
    }
}
