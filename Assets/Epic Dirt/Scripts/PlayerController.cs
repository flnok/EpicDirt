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
    public float speed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    public Boundary boundary;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }
    void FixedUpdate()
    {
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax));
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
