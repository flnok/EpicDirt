using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    private PlayerStats GetPlayerStatus;
    private EnemyStats GetEnemyStatus;

    public AIPath aiPath;
    private Animator GetAnimator;
    private AIDestinationSetter aIDestination;

    public GameObject TextHiting;
    private GameObject GetGameController;

    private void Awake()
    {
        GetGameController = GameObject.FindGameObjectWithTag("GameController");
        GetAnimator = GetComponent<Animator>();
        GetPlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        GetEnemyStatus = GetComponent<EnemyStats>();

        aIDestination = GetComponentInParent<AIDestinationSetter>();
        aIDestination.target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(gameObject.name == "Zombie")
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        
    }

    public void Dead()
    {
        GetComponent<EnemyController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject.transform.parent.gameObject);
    }

    private int JUST_DIE_ONCE = 1;

    //Get Damge//
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            if (GetEnemyStatus.health > 0)
            {
                GetAnimator.SetFloat("Hit", 1f);

                // percent of critical : 20%
                bool isCritical = Random.Range(0, 100) < 20;
                float damge = PlayerStats.Damge;
                if (isCritical)
                {
                    damge *= 2f;
                }
                GetEnemyStatus.health -= damge;

                CreatePopup(transform.position, damge, isCritical);

                GetAnimator.SetFloat("Hit", 0f);
            }
            if (GetEnemyStatus.health < 1 && JUST_DIE_ONCE == 1)
            {
                int idEnemy = GetEnemyStatus.IdEnemy;
                if (idEnemy == 1)
                {
                    // melee
                    PlayerStats.Experience += 0.1f;
                    GetGameController.GetComponent<GameController>().UpdateScore(30);
                }
                else if(idEnemy == 2)
                {
                    // range
                    PlayerStats.Experience += 0.15f;
                    GetGameController.GetComponent<GameController>().UpdateScore(40);
                }
                else if(idEnemy == 3)
                {
                    PlayerStats.Experience += 0.3f;
                    GetGameController.GetComponent<GameController>().UpdateScore(100);
                }

                GetAnimator.SetTrigger("Dead");
                JUST_DIE_ONCE++;
            }
        }
    }

    public void CreatePopup(Vector3 position, float damageAmount, bool isCriticalHit)
    {
        GameObject PopupTransform = Instantiate(TextHiting, position, Quaternion.identity);
        PopupTransform.GetComponent<PopupDamge>().Setup(damageAmount, isCriticalHit);
    }
}
