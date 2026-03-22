using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLock : MonoBehaviour
{
    public int levelIndex;
    public GameObject lockIcon;
    public Button button;

    void Start()
    {
        bool unlocked = IsUnlocked();

        lockIcon.SetActive(!unlocked);
        button.interactable = unlocked;
    }

    bool IsUnlocked()
    {
        // Level 1 always unlocked
        if (levelIndex == 1)
            return true;

        // unlock if previous level has stars
        int previousStars = PlayerPrefs.GetInt("Level" + (levelIndex - 1), 0);

        return previousStars > 0;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
