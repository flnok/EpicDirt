using UnityEngine;
using System.Collections;

public class PickUpController : MonoBehaviour
{
    public GameObject ObjectEffect;
    private ItemsEffect EffectScript;

    private void Start()
    {
        EffectScript = GetComponent<ItemsEffect>();
    }

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
        GameObject newEffect = Instantiate(ObjectEffect) as GameObject;
        newEffect.transform.position = new Vector2(transform.position.x, transform.position.y);

        //Power up
        PowerUp(player.GetComponent<PlayerStats>());

        //Destroy
        Destroy(gameObject);                
    }

    private void PowerUp(PlayerStats stats)
    {
        int EffectIndex = Random.Range(0, EffectScript.EffectAmount);
        switch (EffectIndex)
        {
            case 0:
                if (stats.AttackSpeed > 0.3)
                    stats.AttackSpeed = EffectScript.BoostAttackSpeed(stats.AttackSpeed);

                break;
            case 1:
                if (stats.MoveSpeed < 7)
                    stats.MoveSpeed = EffectScript.BoostMoveSpeed(stats.MoveSpeed);

                break;
            case 2:
                if (stats.Health < 1)
                    stats.Health = EffectScript.BoostHealth(stats.Health);

                break;
            case 3:
                if (stats.Damge < 20)
                    stats.Damge = EffectScript.BoostDamge(stats.Damge);

                break;
            case 4:
                if (stats.NumberShot < 2)
                    stats.NumberShot = EffectScript.AddDoubleHit(stats.NumberShot);

                break;
        }
    }
}