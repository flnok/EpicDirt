using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsEffect : MonoBehaviour
{
    public int EffectAmount { get; set; } = 5;

    public float BoostAttackSpeed(float attackspeed) => attackspeed -= 0.1f;
    public float BoostMoveSpeed(float movespeed) => movespeed += 1f;
    public float BoostHealth(float health) => health += 0.2f;
    public float BoostDamge(float damge) => damge += 2;
    public int AddDoubleHit(int hiteffect) => hiteffect += 1;
}
