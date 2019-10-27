using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyStats : MonoBehaviour
{
    public int IdEnemy;
    public float health, aspeed, mspeed, damge;
    public AIPath aipath;

	void Awake()
    {
        if(IdEnemy == 1)   //melee
        {
            health = 50f;
            aspeed = 0.4f;
            mspeed = 3f;
            damge = 7f;
            aipath.maxSpeed = mspeed;
        }
        else    //range
        {
            health = 40f;
            aspeed = 0.4f;
            mspeed = 3;
            damge = 10f;
            aipath.maxSpeed = mspeed;
        }
	}
}
