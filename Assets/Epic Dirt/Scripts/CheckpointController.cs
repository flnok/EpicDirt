using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    public LevelChanger lc;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Enter();
        }
    }

    private void Enter()
    {
        //Stop character
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;

        //Effect         //LoadScene
        lc.fadeLevel();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
