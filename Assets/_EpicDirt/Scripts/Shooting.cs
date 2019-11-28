using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject purpleBullet;
    public float bulletForce = 20f;

    private float attackSpeed;
    private float nextFire;
    private Transform firePoint;

    public bool isPowerUP = false;

    void Start()
    {
        firePoint = transform.GetChild(0).transform;
    }

    void Update()
    {
        attackSpeed = PlayerStats.AttackSpeed;
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + attackSpeed ;
            StartCoroutine(Attack());
        }
    }
     
    IEnumerator Attack()
    {
        Shoot();
        if(PlayerStats.NumberShot == 2)
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
            GameObject bl = Instantiate(purpleBullet, firePoint.position, firePoint.rotation);
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
