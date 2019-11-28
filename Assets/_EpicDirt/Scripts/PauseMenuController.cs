using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    private static bool Gameispauses = false;
    private GameController GetGameController;
    public GameObject menuUI;
    public GameObject endUI;
    private static float currentVolume;
    public VolumeController GetVolume;

    private void Awake()
    {
        GetGameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        currentVolume = AudioListener.volume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Gameispauses)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        menuUI.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().enabled = true;
        Time.timeScale = 1f;
        Gameispauses = false;
    }

    public void Pause()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().enabled = false;
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        Gameispauses = true;
    }

    public void GotoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ReloadScence() => GetGameController.RestartLevel();

    // call from playercontroller when dead animation trigged
    public void GetEndUI()
    {
        endUI.SetActive(true);
        GetComponent<PauseMenuController>().enabled = false;
    }

    //quit game
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }

    // mute
    public void Mute(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetFloat("SliderVolumeLevel", 0f);
        }
        else
        {
            if(currentVolume == 0)
            {
                currentVolume = 1f;
            }
            PlayerPrefs.SetFloat("SliderVolumeLevel", currentVolume);
        }
    }
}
