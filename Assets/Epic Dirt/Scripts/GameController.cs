using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject activateObject;
    public float duration = 30f;
    public GameObject instrucstion;
    void Awake()
    {
        activateObject.SetActive(true);
    }

    void Start()
    {
        StartCoroutine(StopInstruction());
    }

    IEnumerator StopInstruction()
    {
        yield return new WaitForSecondsRealtime(duration);
        instrucstion.SetActive(false);
    }
}