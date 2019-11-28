using System.Collections;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
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
    public AudioClip otherClip;
    public AudioClip dieClip;
    AudioSource audioSource;


    private float touched;
    private float timeneeded;

    private void Awake()
    {
        GetCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        firePoint = transform.GetChild(0).transform;
    }

    private void Start()
    {
        
        durationPowerUP = 10f;
        touched = 0f;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        speed = PlayerStats.MoveSpeed;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = GetCamera.ScreenToWorldPoint(Input.mousePosition);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);


        if (!audioSource.isPlaying)
        {
            audioSource.clip = otherClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        
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

    //Hit Player//
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            timeneeded = collision.GetComponent<EnemyStats>().aspeed;
            touched += Time.fixedDeltaTime;
            if (Mathf.Approximately(touched, timeneeded))
            {
                HitPlayer(collision.gameObject);
                touched = 0f;
            }
        }
    }

    //Hit Player//

    private void HitPlayer(GameObject enemy)
    {
        if (PlayerStats.Health > 0)
        {
            float damge = enemy.GetComponent<EnemyStats>().damge;
            bool isCritical = false;
            enemy.GetComponent<EnemyController>().CreatePopup(transform.position, damge, isCritical);
            PlayerStats.Health -= damge / 100;
        }
        if (PlayerStats.Health < 0.01)
        {
            animator.SetTrigger("Dead");
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(go);
            }
            GetComponent<Shooting>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
        }
    }

    // call when player dead animation done 
    public void gotohell()
    {
        audioSource.Stop();
        audioSource.clip = dieClip;
        audioSource.loop = false;
        audioSource.Play();
        Time.timeScale = 0f;
        GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenuController>().GetEndUI();
    }

    // call from PlayerStats
    public void PowerUP()
    {
        //play sound
        GetComponents<AudioSource>()[1].Play();
        // create effect power up
        Instantiate(pfPowerUP, transform.position, transform.rotation);
        PlayerStats.Damge *= 2;
        PlayerStats.Health += 0.5f;
        transform.localScale += Vector3.one;
        GetComponent<Shooting>().isPowerUP = true; // increase size of bullet
        PlayerStats.MoveSpeed -= 1; // slow down


        // wait
        StartCoroutine(PowerDOWN());
    }

    IEnumerator PowerDOWN()
    {
        yield return new WaitForSeconds(durationPowerUP);
        // get power back
        PlayerStats.Damge /= 2;
        PlayerStats.Health -= 0.5f;
        transform.localScale -= Vector3.one;
        GetComponent<Shooting>().isPowerUP = false; // decrease size of bullet
        PlayerStats.MoveSpeed += 1;
    }
}
