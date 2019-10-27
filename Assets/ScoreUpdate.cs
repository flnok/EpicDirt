using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    private Text textScore;

    private void Start()
    {
        textScore = transform.GetComponent<Text>();
        textScore.text = "Score: 0";
    }

    public void SetScore(int score) => textScore.text = "Score: " + score.ToString();
}
