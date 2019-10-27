using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    private float touched;
    private float timeneeded;
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

    void Start()
    {
        touched = 0f;
        timeneeded = GetComponent<EnemyStats>().aspeed;
    }
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void Dead() => Destroy(gameObject.transform.parent.gameObject, 0.1f);

    private int JUST_DIE_ONCE = 1;
    //Get Damge//
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            if (GetEnemyStatus.health > 0)
            {
                GetAnimator.SetFloat("Hit", 1f);

                bool isCritical = UnityEngine.Random.Range(0, 100) < 20;
                float damge = GetPlayerStatus.Damge;
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
                    //canchien
                    GetPlayerStatus.Experience += 0.1f;
                    GetGameController.GetComponent<GameController>().UpdateScore(30);
                }
                else
                {
                    //danhxa
                    GetPlayerStatus.Experience += 0.15f;
                    GetGameController.GetComponent<GameController>().UpdateScore(40);
                }

                GetAnimator.SetTrigger("Dead");
                JUST_DIE_ONCE++;
            }
        }
    }

    private void CreatePopup(Vector3 position, float damageAmount, bool isCriticalHit)
    {
        GameObject PopupTransform = Instantiate(TextHiting, position, Quaternion.identity);
        PopupTransform.GetComponent<PopupDamge>().Setup(damageAmount, isCriticalHit);
    }

    //Hit Player//
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            touched += Time.fixedDeltaTime;
            if (Mathf.Approximately(touched, timeneeded))
            {
                GetAnimator.SetFloat("Attack",1f);
                HitPlayer(collision.collider.gameObject);
                touched = 0f;
                GetAnimator.SetFloat("Attack", 0.1f);
            }
        }
    }

    private void HitPlayer(GameObject player)
    {
        if(GetPlayerStatus.Health > 0)
        {
            float damge = GetEnemyStatus.damge;
            bool isCritical = false;
            CreatePopup(transform.position, damge, isCritical);
            GetPlayerStatus.Health -= damge / 100;
        }
        if (GetPlayerStatus.Health < 0.01)
        {
            player.GetComponent<Animator>().SetTrigger("Dead");
            Time.timeScale = 0f;
        }
    }
}
