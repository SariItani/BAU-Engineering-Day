
using UnityEngine;

public class BoundedObject : MonoBehaviour
{

    public Transform target;
    public BoundedObject ref_obj;
    public float x_min;
    public float x_max;
    public float y_min;
    public float y_max;
    void Update()
    {
        if (ref_obj)
        {
            // get bounds from reference object if it exists
            // useful for copying defined bounds from the player or the camera.
            x_min = ref_obj.x_min;
            x_max = ref_obj.x_max;
            y_min = ref_obj.y_min;
            y_max = ref_obj.y_max;
        }
        float final_x = (x_max != 0) && (x_min != 0) ? Mathf.Clamp(target.position.x, x_min, x_max) : target.position.x;
        float final_y = (y_max != 0) && (y_min != 0) ? Mathf.Clamp(target.position.y, y_min, y_max) : target.position.y;
        transform.position = new Vector3(
           final_x,
           final_y,
           transform.position.z
        );
    }

}
