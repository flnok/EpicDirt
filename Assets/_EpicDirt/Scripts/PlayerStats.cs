using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Image HealthBar;
    public Image ExperienceBar;


    public float Health;
    public float MoveSpeed;
    public float AttackSpeed;
    public float Damge;
    public int NumberShot;

    public float Experience;

    void Update()
    {
        HealthBar.fillAmount = Health;
        ExperienceBar.fillAmount = Experience;
        if (Experience > 0.9f)
        {
            GetComponent<PlayerController>().PowerUP();
            Experience = 0f;
        }
    }
}
