using UnityEngine;

public class LevelSelectStars : MonoBehaviour
{
    public int levelIndex;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    void Start()
    {
        int stars = PlayerPrefs.GetInt("Level" + levelIndex, 0);

        star1.SetActive(stars >= 1);
        star2.SetActive(stars >= 2);
        star3.SetActive(stars >= 3);
    }
}
