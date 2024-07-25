using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject OptionsPanel;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public delegate void OnMusicVolumeChange(float volume);
    public delegate void OnSFXVolumeChange(float volume);
    public static OnMusicVolumeChange OnMusicVolumeChanged;
    public static OnSFXVolumeChange OnSFXVolumeChanged;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.GetAudioManager();
        MainMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        InitializeSliders();
    }

    void InitializeSliders()
    {
        musicSlider.value = audioManager.GetMusicVolume();
        sfxSlider.value = audioManager.GetSFXVolume();

        Debug.Log($"Initialized sliders: Music Volume = {musicSlider.value}, SFX Volume = {sfxSlider.value}");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsMenu()
    {
        MainMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void SetMusicVolume(float volume)
    {
        if (OnMusicVolumeChanged != null)
        {
            OnMusicVolumeChanged.Invoke(volume);
            Debug.Log("Music volume changed to: " + volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (OnSFXVolumeChanged != null)
        {
            OnSFXVolumeChanged.Invoke(volume);
        }
    }
}
