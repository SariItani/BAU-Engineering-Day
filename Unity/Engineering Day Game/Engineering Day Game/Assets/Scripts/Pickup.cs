using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    GameObject player;
    SpriteRenderer spriteRenderer;
    public Image item;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        item = GameObject.Find("Item").GetComponent<Image>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        if (collision.gameObject == player)
        {
            giveImage();
            player.GetComponent<PlayerController>().SetShoot();
            Destroy(gameObject);
        }
    }

    public void giveImage()
    {
        item.sprite = spriteRenderer.sprite;
        item.color = new Color(1f, 1f, 1f, 1f);
    }
}
