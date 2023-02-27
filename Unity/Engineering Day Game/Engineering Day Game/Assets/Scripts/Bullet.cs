using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int bullet_damage = 25;
    public AudioSource audioData;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Image item;
    public float speed = 20;
    public LayerMask layer;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        item = GameObject.Find("Item").GetComponent<Image>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;
        rb.velocity = transform.right * speed;
        item.sprite = null;
        item.color = new Color(1f, 1f, 1f, 0f);
        audioData = gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DamageableObject>() != null)
        {
            // Debug.Log("Enemy hit");
            audioData.Play();
            other.GetComponent<DamageableObject>().TakeDamage(bullet_damage);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.AttackDelegate = player.Punch;
    }
}
