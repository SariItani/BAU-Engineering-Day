using UnityEngine;

public class FollowPlayer : BoundedObject
{
    public float req_y;
    public float req_x;

    void LateUpdate()
    {
        // add option to set camera at a specific coordinate, and then clamp the
        // player's movement to prevent them from going off camera
        float required_x = req_x != 0 ? req_x : transform.position.x;
        float required_y = req_y != 0 ? req_y : transform.position.y;
        transform.position = new Vector3(required_x, required_y, transform.position.z);
    }
}
