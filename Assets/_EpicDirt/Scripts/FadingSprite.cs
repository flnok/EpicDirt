using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingSprite : MonoBehaviour
{
    private SpriteRenderer ChangeColorAlpha;

    private void Start()
    {
        ChangeColorAlpha = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
        {
            Color c = ChangeColorAlpha.color;
            c.a = 0.5f;
            ChangeColorAlpha.color = c;
            collision.GetComponent<SpriteRenderer>().color = c;
            collision.GetComponentInChildren<SpriteRenderer>().color = c;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Color c = ChangeColorAlpha.color;
        c.a = 1f;
        ChangeColorAlpha.color = c;
        collision.GetComponent<SpriteRenderer>().color = c;
        collision.GetComponentInChildren<SpriteRenderer>().color = c;
    }
}
