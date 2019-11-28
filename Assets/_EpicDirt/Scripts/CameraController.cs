using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private Transform player;
    public float smoothTime = 2;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = player.position - transform.position;
    }

    void Update()
    {
        if(player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.position - offset, Time.deltaTime * smoothTime);
        }
    }

}
