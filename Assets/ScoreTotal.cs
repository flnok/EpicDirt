using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTotal : MonoBehaviour
{
    private Text scoreText;
    private int score;

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "0";
    }

    void Update()
    {
        score = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().totalScore;
        scoreText.text = score.ToString();
    }
}
