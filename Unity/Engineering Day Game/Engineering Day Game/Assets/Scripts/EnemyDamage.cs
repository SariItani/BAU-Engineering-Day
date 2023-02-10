using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public HealthBar _healthbar;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.transform.GetComponent<DamageableObject>();
            player.TakeDamage(damage);
            _healthbar.SetHealth(player.currentHealth);

        }
    }
}
