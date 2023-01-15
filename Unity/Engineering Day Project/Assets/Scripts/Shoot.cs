using UnityEngine;

public class Shoot : MonoBehaviour
{
    public SpriteRenderer sr;
    public float bullet_speed;


    void OnTriggerEnter()
    {
        Destroy(this);
    }
}
