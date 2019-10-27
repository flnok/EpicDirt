using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;
    private int level;
    public Text text;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void fadeLevel() => setTriggerFade(SceneManager.GetActiveScene().buildIndex + 1);

    public void setTriggerFade(int index)
    {
        animator = GetComponent<Animator>();
        level = index;
        updateLevel();
        animator.SetTrigger("Fade");
    }

    public void onFadeComplete() => SceneManager.LoadScene(level);

    public void updateLevel() => text.text = "Level " + level;
}
