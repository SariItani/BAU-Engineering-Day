using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int bullet_damage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<DamagableObject>().TakeDamage(bullet_damage);
        Destroy(gameObject);
    }
    // Update is called once per frame
}
