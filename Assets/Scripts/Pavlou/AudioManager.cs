using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager GetAudioManager()
    {
        return instance;
    }

    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] AudioSource sfxAudioSource;

    [SerializeField] AudioClip mainMenuAudioClip;
    [SerializeField] AudioClip inGameAudioClip;
    [SerializeField] AudioClip gameOverAudioClip;
    [SerializeField] AudioClip winAudioClip;
    [SerializeField] AudioClip questCompleted;

    private bool isPaused = false;
    float userMusicVolumePreferred = 0.5f;
    float userSFXVolumePreferred = 0.5f;

    private AudioState currentState;

    public enum AudioState
    {
        MainMenu,
        InGame,
        GameOver,
        Win,
        Pause
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadSettings();
    }

    void Start()
    {
        
    }

    void OnEnable()
    {
        Menu.OnMusicVolumeChanged += SetMusicVolume;
        Menu.OnSFXVolumeChanged += SetSFXVolume;
    }

    void OnDisable()
    {
        Menu.OnMusicVolumeChanged -= SetMusicVolume;
        Menu.OnSFXVolumeChanged -= SetSFXVolume;
    }

    void OnApplicationQuit()
    {
        SaveSettings();
    }

    void Update()
    {
        switch (currentState)
        {
            case AudioState.MainMenu:
                PlayMenuMusic();
                break;
            case AudioState.InGame:
                break;
            case AudioState.GameOver:
                PlayGameOverMusic();
                break;
            case AudioState.Win:
                break;
            case AudioState.Pause:
                break;
            default:
                break;
        }
    }

    void PlayMenuMusic()
    {
        if (!mainAudioSource.isPlaying)
        {
            mainAudioSource.clip = mainMenuAudioClip;
            mainAudioSource.Play();
        }
    }

    void PlayInGameMusic()
    {
        if (isPaused)
        {
            mainAudioSource.volume = 0.1f;
        }
        else
        {
            mainAudioSource.UnPause();
        }

        if (!sfxAudioSource.isPlaying)
        {
            sfxAudioSource.clip = inGameAudioClip;
            sfxAudioSource.Play();
        }
    }

    void PlayGameOverMusic()
    {
        if (currentState == AudioState.GameOver && !mainAudioSource.isPlaying)
        {
            mainAudioSource.clip = gameOverAudioClip;
            mainAudioSource.Play();
        }
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            userMusicVolumePreferred = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            Debug.Log("No saved music volume found, using default value");
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            userSFXVolumePreferred = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            Debug.Log("No saved SFX volume found, using default value");
        }

        mainAudioSource.volume = userMusicVolumePreferred;
        sfxAudioSource.volume = userSFXVolumePreferred;

        Debug.Log($"Loaded settings: Music Volume = {userMusicVolumePreferred}, SFX Volume = {userSFXVolumePreferred}");
    }

    void SaveSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", userMusicVolumePreferred);
        PlayerPrefs.SetFloat("sfxVolume", userSFXVolumePreferred);
        PlayerPrefs.Save();  // Make sure to save PlayerPrefs

        Debug.Log($"Saved settings: Music Volume = {userMusicVolumePreferred}, SFX Volume = {userSFXVolumePreferred}");
    }

    void SetMusicVolume(float value)
    {
        mainAudioSource.volume = value;
        userMusicVolumePreferred = value;
        SaveSettings();
    }

    void SetSFXVolume(float value)
    {
        sfxAudioSource.volume = value;
        userSFXVolumePreferred = value;
        SaveSettings();
    }

    public float GetMusicVolume()
    {
        return userMusicVolumePreferred;
    }

    public float GetSFXVolume()
    {
        return userSFXVolumePreferred;
    }
}
