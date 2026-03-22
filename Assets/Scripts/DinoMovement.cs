using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DinoMovement : MonoBehaviour
{
    Animator anim;
    Rigidbody2D r;
    public CharacterController2D controller;
    public float maxhealthValue = 100;
    float currentHealth;
    public Slider healthSlider;
    float scoreValue = 0;
    public Text ScoreText;
    bool jump = false;
    bool crouch = false;
    float leftMove = -50f;
    float RightMove = 50f;
    public AudioClip AppleSound;
    public AudioClip CoinSound;
    public GameObject cloud;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public BoxCollider2D cameraBounds;
    private float minX, maxX;
    public float fallThreshold = -5f;
    public GameObject replayPanel;
    public GameObject levelCompletePanel;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        levelCompletePanel.SetActive(false);

        anim = GetComponent<Animator>();
        r = GetComponent<Rigidbody2D>();

        ScoreText.text = "Score: " + scoreValue.ToString();

        currentHealth = maxhealthValue;
        healthSlider.maxValue = maxhealthValue;
        healthSlider.value = currentHealth;

        Bounds bounds = cameraBounds.bounds;
        minX = bounds.min.x;
        maxX = bounds.max.x;

        replayPanel.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetBool("walk", false);

        var touch = Touchscreen.current?.primaryTouch;

        if (touch != null && touch.press.isPressed)
        {
            float screenMid = Screen.width / 2;
            float posX = touch.position.ReadValue().x;

            if (posX > screenMid)
            {
                controller.Move(RightMove * Time.deltaTime, crouch, jump);
                anim.SetBool("walk", true);
            }
            else
            {
                controller.Move(leftMove * Time.deltaTime, crouch, jump);
                anim.SetBool("walk", true);
            }
        }
        else
        {
            anim.SetBool("walk", false);
        }

    }

    private void Update()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        transform.position = clampedPosition;

        if (transform.position.y < fallThreshold)
        {
            GetComponent<DinoMovement>().enabled = false;
            replayPanel.SetActive(true);
        }


        var touch = Touchscreen.current?.primaryTouch;

        if (touch == null) return;

        // touch start
        if (touch.press.wasPressedThisFrame)
        {
            startTouchPosition = touch.position.ReadValue();
        }

        // touch release
        if (touch.press.wasReleasedThisFrame)
        {
            endTouchPosition = touch.position.ReadValue();

            Vector2 swipeDirection = endTouchPosition - startTouchPosition;

            if (swipeDirection.y > 100f && Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
            {
                r.linearVelocity = new Vector2(r.linearVelocity.x, 6.5f);
                anim.SetTrigger("jump");
            }
        }


        if (currentHealth <= 0)
        {
            anim.SetTrigger("dead");
            GetComponent<DinoMovement>().enabled = false;
            replayPanel.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.StartsWith("Enemy"))
        {
            currentHealth -= 30f;
            healthSlider.value = currentHealth;

            if (currentHealth <= 10)
            {
                anim.SetTrigger("dead");
                GetComponent<DinoMovement>().enabled = false;
                replayPanel.SetActive(true);
            }
        }

        if (col.gameObject.name.StartsWith("Apple"))
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(AppleSound);
            }

            currentHealth += 10;
            healthSlider.value = currentHealth;
            Destroy(col.gameObject);

        }

        if (col.gameObject.name.StartsWith("Coin"))
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(CoinSound);
            }

            scoreValue += 5;
            ScoreText.text = "Score :" + scoreValue.ToString();
            Destroy(col.gameObject);
        }

        if (col.gameObject.name.StartsWith("Cloud"))
        {
            transform.gameObject.transform.parent = cloud.transform;
        }

        else
        {
            transform.gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Door"))
        {
            levelCompletePanel.SetActive(true);
            Time.timeScale = 0f;

            ShowStars();
        }
    }

    public void ShowStars()
    {
        int starsEarned = CalculateStars((int)currentHealth);

        if (starsEarned >= 1)
            star1.SetActive(true);
        if (starsEarned >= 2)
            star2.SetActive(true);
        if (starsEarned == 3)
            star3.SetActive(true);

        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        int previousBest = PlayerPrefs.GetInt("Level" + levelIndex, 0);

        if (starsEarned > previousBest)
        {
            PlayerPrefs.SetInt("Level" + levelIndex, starsEarned);
        }
    }

    public int CalculateStars(int currentHealth)
    {
        if (currentHealth >= 100)
            return 3;

        else if (currentHealth >= 50)
            return 2;

        else if (currentHealth >= 10)
            return 1;
        else
            return 0;
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
