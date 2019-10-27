using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerStats>();
        enemy.GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
