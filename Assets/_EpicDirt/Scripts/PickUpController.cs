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
            PickUp();
        }
    }

    private void PickUp()
    {
        //Effect
        GameObject newEffect = Instantiate(ObjectEffect) as GameObject;
        newEffect.transform.position = new Vector2(transform.position.x, transform.position.y);

        //Power up
        PowerUp();

        //Destroy
        Destroy(gameObject);                
    }

    private void PowerUp()
    {
        int EffectIndex = Random.Range(0, EffectScript.EffectAmount);
        switch (EffectIndex)
        {
            case 0:
                if (PlayerStats.AttackSpeed > 0.3)
                    PlayerStats.AttackSpeed = EffectScript.BoostAttackSpeed(PlayerStats.AttackSpeed);

                break;
            case 1:
                if (PlayerStats.MoveSpeed < 7)
                    PlayerStats.MoveSpeed = EffectScript.BoostMoveSpeed(PlayerStats.MoveSpeed);

                break;
            case 2:
                if (PlayerStats.Health < 1)
                    PlayerStats.Health = EffectScript.BoostHealth(PlayerStats.Health);

                break;
            case 3:
                if (PlayerStats.Damge < 20)
                    PlayerStats.Damge = EffectScript.BoostDamge(PlayerStats.Damge);

                break;
            case 4:
                if (PlayerStats.NumberShot < 2)
                    PlayerStats.NumberShot = EffectScript.AddDoubleHit(PlayerStats.NumberShot);
                  break;
        }
    }
}