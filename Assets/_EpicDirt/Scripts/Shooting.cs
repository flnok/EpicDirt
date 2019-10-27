using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float bulletForce = 20f;

    private PlayerStats GetPlayerStatus;
    private float attackSpeed;
    private float nextFire;
    private Transform firePoint;

    public bool isPowerUP = false;

    private void Awake()
    {
        GetPlayerStatus = GetComponent<PlayerStats>();
        firePoint = transform.GetChild(0).transform;
    }

    void Start()
    {
        attackSpeed = GetPlayerStatus.AttackSpeed;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + attackSpeed ;
            StartCoroutine(Attack());
        }
    }
     
    IEnumerator Attack()
    {
        Shoot();
        if(GetPlayerStatus.NumberShot == 2)
        {
            yield return new WaitForSeconds(0.1f);
            Shoot();
        }
    }

    private void Shoot()
    {
        if (isPowerUP)
        {
            // power up
            GameObject bl = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Physics2D.IgnoreCollision(bl.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bl.transform.localScale += Vector3.one;
            Rigidbody2D rbbl = bl.GetComponent<Rigidbody2D>();
            rbbl.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            // default
            GameObject bl = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Physics2D.IgnoreCollision(bl.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Rigidbody2D rbbl = bl.GetComponent<Rigidbody2D>();
            rbbl.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }

    }
}
