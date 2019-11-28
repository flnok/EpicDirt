using UnityEngine;
using Pathfinding;

public class EnemyStats : MonoBehaviour
{
    public int IdEnemy;
    // 1 : Melee
    // 2 : SuperMelee
    // 3 : Boss
    public float health, aspeed, mspeed, damge;
    public AIPath aipath;

	void Start()
    {
        switch (IdEnemy)
        {
            case 1:
                health = 50f;
                aspeed = 0.5f;
                mspeed = 4f;
                damge = 7f;
                aipath.maxSpeed = mspeed;
                break;
            case 2:
                health = 40f;
                aspeed = 0.5f;
                mspeed = 6;
                damge = 10f;
                aipath.maxSpeed = mspeed;
                break;
            case 3:
                health = 200f;
                aspeed = 0.7f;
                mspeed = 4f;
                damge = 35f;
                aipath.maxSpeed = mspeed;
                break;
        }
    }
}
