using UnityEngine;

public class FollowPlayer : BoundedObject
{
    public float req_y;

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, req_y, transform.position.z);
    }
}
