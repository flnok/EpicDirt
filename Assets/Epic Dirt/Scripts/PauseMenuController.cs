using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool Gameispauses = false;
    public GameObject menuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Gameispauses)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        Gameispauses = false;
    }

    void Pause()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        Gameispauses = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
    }

    public void gotoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void reloadScence()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
