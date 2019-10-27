using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelChanger levelChanger;

    public void StartGame() => levelChanger.fadeLevel();
    public void QuitGame() => Application.Quit();
}
