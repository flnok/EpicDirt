using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject HitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player") && !collision.collider.CompareTag("Bullet"))
        {
            Instantiate(HitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
