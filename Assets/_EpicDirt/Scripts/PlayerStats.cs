using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public Image HealthBar;
    public Image ExperienceBar;


    public static float Health = 1f;
    public static float MoveSpeed = 5f;
    public static float AttackSpeed = 0.5f;
    public static float Damge = 10f;
    public static int NumberShot = 1;
    public static float Experience;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Health = 1f;
            MoveSpeed = 5f;
            AttackSpeed = 0.5f;
            Damge = 10f;
            NumberShot = 1;
            Experience = 0f;
        }
    }

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
