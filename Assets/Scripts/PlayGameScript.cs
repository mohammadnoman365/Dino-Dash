using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGameScript : MonoBehaviour
{
    public Button replayButton;

    public void playGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}
