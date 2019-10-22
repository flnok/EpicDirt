using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int level;
    public Text text;

    public void fadeLevel()
    {
        setTriggerFade(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void setTriggerFade(int index)
    {
        level = index;
        updateLevel();
        animator.SetTrigger("Fade");
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(level);
    }

    public void updateLevel()
    {
        text.text = "Level " + level;
    }
}
