using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GameObject objecteffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp(collision);
        }
    }

    private void PickUp(Collider2D player)
    {
        //Effect
        GameObject newEffect = Instantiate(objecteffect) as GameObject;
        newEffect.transform.position = new Vector2(transform.position.x, transform.position.y);
        //Power up
        //Instantiate()
        //Destroy
        Destroy(gameObject);                
    }
}