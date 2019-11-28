using UnityEngine;
using UnityEngine.UI;

public class ScoreTotal : MonoBehaviour
{
    private Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = GameController.totalScore.ToString();
    }
}
