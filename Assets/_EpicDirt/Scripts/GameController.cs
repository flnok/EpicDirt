using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject GetLevelChanger;
    public GameObject instrucstion;
    public GameObject[] spawnObjects;
    public float xMin, xMax, yMin, yMax;

    public static int totalScore = 0;
    public float durationInstruction = 10f;


    void Start()
    {
        GetLevelChanger.SetActive(true);
        SpawnObject();

        // instruction at level 1   
        if (instrucstion.activeSelf)
        {
            StartCoroutine(StopInstruction());
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            totalScore = 0;
        }
    }

    IEnumerator StopInstruction()
    {
        yield return new WaitForSecondsRealtime(durationInstruction);
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
    public void CompleteLevel()
    {
        // Play permently
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            GetLevelChanger.GetComponent<LevelChanger>().setTriggerFade(2);
        }
        else
        {
            GetLevelChanger.GetComponent<LevelChanger>().FadeLevel();
        }
    }

    // change score text
    public void UpdateScore(int score)
    {
        totalScore += score;
    }

    // call from pausemenucontroller
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        GetLevelChanger.GetComponent<LevelChanger>().setTriggerFade(SceneManager.GetActiveScene().buildIndex);
    }

    // call from pausemenucontroller
    public void PlayFromBegining()
    {
        Time.timeScale = 1f;
        GetLevelChanger.GetComponent<LevelChanger>().setTriggerFade(1);
    }
}