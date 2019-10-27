using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject GetLevelChanger;
    public GameObject instrucstion;
    public GameObject[] spawnObjects;
    public float xMin, xMax, yMin, yMax;

    private GameObject GetScore;
    public int totalScore;
    private float duration = 10f;


    private void Awake()
    {
        GetScore = GameObject.FindGameObjectWithTag("ScoreText");
    }

    void Start()
    {
        totalScore = 0;
        GetLevelChanger.SetActive(true);
        SpawnObject();

        // instruction at level 1
        if (instrucstion.activeSelf)
        {
            StartCoroutine(StopInstruction());
        }
    }

    IEnumerator StopInstruction()
    {
        yield return new WaitForSecondsRealtime(duration);
        instrucstion.SetActive(false);
    }

    // random create items and hazard
    private void SpawnObject()
    {
        int spawned = 0;
        while (spawned < spawnObjects.Length)
        {
            Vector3 position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0f) ;
            Instantiate(spawnObjects[spawned], position, Quaternion.identity);
            spawned++;
        }
    }

    // kill clone bullet
    private void OnTriggerExit2D(Collider2D collision) => Destroy(collision.gameObject);

    // checkpoint
    public void CompleteLevel() => GetLevelChanger.GetComponent<LevelChanger>().fadeLevel();

    // change score text
    public void UpdateScore(int score)
    {
        totalScore += score;
        GetScore.GetComponent<ScoreUpdate>().SetScore(totalScore);
    }

    // call from pausemenucontroller
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        GetLevelChanger.GetComponent<LevelChanger>().setTriggerFade(SceneManager.GetActiveScene().buildIndex);
    }

    // call from pausemenucontroller
    public void PlayFromBegining() => GetLevelChanger.GetComponent<LevelChanger>().setTriggerFade(1);
}