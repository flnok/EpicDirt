
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    public LevelChanger levelChanger;

    public void StartGame() => levelChanger.FadeLevel();
    public void QuitGame() => Application.Quit();
}
