using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject LevelPanel;
    public TextMeshProUGUI starsText;
    public int totalLevels = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MainMenuPanel != null)
            MainMenuPanel.SetActive(true);

        if (LevelPanel != null)
            LevelPanel.SetActive(false);

        if (SettingsPanel != null)
            SettingsPanel.SetActive(false);
    }

    public void ShowLevelsPanel()
    {
        if (MainMenuPanel != null)
            MainMenuPanel.SetActive(false);

        if (LevelPanel != null)
            LevelPanel.SetActive(true);


        int totalStars = 0;

        for (int i = 1; i <= totalLevels; i++)
        {
            totalStars += PlayerPrefs.GetInt("Level" + i, 0);
        }

        if (starsText != null)
            starsText.text = totalStars.ToString();
    }

    public void HideLevelsPanel()
    {
        if (LevelPanel != null)
            LevelPanel.SetActive(false);

        if (MainMenuPanel != null)
            MainMenuPanel.SetActive(true);

    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowSettingsPanel()
    {
        if (SettingsPanel != null)
            SettingsPanel.SetActive(true);
    }
    public void HideSettingsPanel()
    {
        if (SettingsPanel != null)
            SettingsPanel.SetActive(false);
    }
}
