using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private GameObject GetGameController;
    private Animator animator;
    private GameObject[] enemy;
    private bool isopen = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GetGameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemy.Length == 0)
        {
            animator.SetTrigger("Open");
            isopen = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isopen && collision.CompareTag("Player"))
        {
            Enter();
        }
    }

    private void Enter()
    {
        //Stop character
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().enabled = false;


        //LoadScene
        GetGameController.GetComponent<GameController>().CompleteLevel();
    }
}
