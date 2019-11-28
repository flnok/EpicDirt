
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private static int FIRST_START = 1;
    public Slider GetSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SliderVolumeLevel") && FIRST_START == 1)
        {
            PlayerPrefs.DeleteKey("SliderVolumeLevel");
        }
        FIRST_START = 2;

        GetSlider.value = AudioListener.volume;

        DontDestroyOnLoad(gameObject);
    }

    public void SaveSliderValue(Slider slider)
    {
        PlayerPrefs.SetFloat("SliderVolumeLevel", slider.value);
    }

    void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("SliderVolumeLevel", 1f);
    }
}
