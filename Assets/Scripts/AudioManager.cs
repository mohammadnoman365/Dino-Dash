using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;
    public AudioClip level4Music;

    [HideInInspector] public bool musicEnabled = true;
    [HideInInspector] public bool sfxEnabled = true;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!musicEnabled) return;

        switch (scene.buildIndex)
        {
            case 0:
                PlayMusic(mainMenuMusic);
                break;
            case 1:
                PlayMusic(level1Music);
                break;
            case 2:
                PlayMusic(level2Music);
                break;
            case 3:
                PlayMusic(level3Music);
                break;
            case 4:
                PlayMusic(level4Music);
                break;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.Play();
    }


    public void SetMusic(bool enabled)
    {
        musicEnabled = enabled;
        musicSource.mute = !enabled;

        PlayerPrefs.SetInt("Music", enabled ? 1 : 0);
    }

    public void SetSFX(bool enabled)
    {
        sfxEnabled = enabled;
        sfxSource.mute = !enabled;

        PlayerPrefs.SetInt("SFX", enabled ? 1 : 0);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!sfxEnabled || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    void LoadSettings()
    {
        musicEnabled = PlayerPrefs.GetInt("Music", 1) == 1;
        sfxEnabled = PlayerPrefs.GetInt("SFX", 1) == 1;

        musicSource.mute = !musicEnabled;
        sfxSource.mute = !sfxEnabled;
    }
}
