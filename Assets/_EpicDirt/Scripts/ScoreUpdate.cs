using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    private Text scoreText;

    private void Start()
    {
        scoreText = transform.GetComponent<Text>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + GameController.totalScore.ToString();
    }

    //public void SetScore(int score) => textScore.text = "Score: " + score.ToString();
}
