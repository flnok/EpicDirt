using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;
    private int level;
    private int textLevel;
    private static int moreLevel = 5;
    private static int LOOP_LEVEL = 1;
    public Text text;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeLevel() => setTriggerFade(SceneManager.GetActiveScene().buildIndex + 1);

    public void setTriggerFade(int index)
    {
        if(SceneManager.GetActiveScene().buildIndex >= 5)
        {
            LOOP_LEVEL = 2;
        }

        if (LOOP_LEVEL == 2)
        {
            moreLevel += 1;
            textLevel = moreLevel;
        }
        else textLevel = index;

        level = index;
        animator = GetComponent<Animator>();
        updateLevel();
        animator.SetTrigger("Fade");
    }

    public void onFadeComplete() => SceneManager.LoadScene(level);

    public void updateLevel() => text.text = "Level " + textLevel;
}
