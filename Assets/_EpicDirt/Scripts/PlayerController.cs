using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
    private PlayerStats GetPlayerStatus;
    private float speed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private Camera GetCamera;
    private Vector2 mousePos;
    private Transform firePoint;

    public Boundary boundary;
    public GameObject pfPowerUP;
    public float durationPowerUP;

    private void Start()
    {
        GetCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        GetPlayerStatus = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        firePoint = transform.GetChild(0).transform;
        durationPowerUP = 10f;
    }

    private void Update()
    {
        speed = GetPlayerStatus.MoveSpeed;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = GetCamera.ScreenToWorldPoint(Input.mousePosition);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax));
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector2 direction = mousePos - rb.position;

        //change firepoint direction
        firePoint.up = direction.normalized;
    }

    // call when player dead animation done 
    public void gotohell() => GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenuController>().GetEndUI();


    public void PowerUP()
    {
        // create effect power up
        Instantiate(pfPowerUP, transform.position, transform.rotation);
        GetPlayerStatus.Damge *= 2;
        GetPlayerStatus.Health += 0.5f;
        transform.localScale += Vector3.one;
        GetComponent<Shooting>().isPowerUP = true; // increase size of bullet
        GetPlayerStatus.MoveSpeed -= 1; // slow down


        // wait
        StartCoroutine(PowerDOWN());
    }

    IEnumerator PowerDOWN()
    {
        yield return new WaitForSeconds(durationPowerUP);
        // get power back
        GetPlayerStatus.Damge /= 2;
        GetPlayerStatus.Health -= 0.5f;
        transform.localScale -= Vector3.one;
        GetComponent<Shooting>().isPowerUP = false; // decrease size of bullet
        GetPlayerStatus.MoveSpeed += 1;
    }
}
