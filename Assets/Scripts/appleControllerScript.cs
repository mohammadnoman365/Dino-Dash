using UnityEngine;

public class appleControllerScript : MonoBehaviour
{
    public GameObject apple;
    public GameObject coin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i <= 10; i++)
        {
            Vector2 applepos = new Vector2(Random.Range(-5, 60), Random.Range(2, 4));
            Instantiate(apple, applepos, Quaternion.identity);

            Vector2 coinpos = new Vector2(Random.Range(-5, 66), Random.Range(2, 4));
            Instantiate(coin, coinpos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}