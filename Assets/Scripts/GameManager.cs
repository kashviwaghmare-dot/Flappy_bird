using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Speed Settings")]
    [Tooltip("Speed at which pipes and obstacles move.")]
    public float gameSpeed = 3f;

    [Header("UI Elements")]
    [Tooltip("Text element for showing the current score.")]
    public TextMeshProUGUI scoreText;

    [Tooltip("Panel displayed when the game starts.")]
    public GameObject startScreen;

    [Tooltip("Panel displayed when the player dies.")]
    public GameObject gameOverScreen;

    [Tooltip("Text element displaying the final score on the Game Over screen.")]
    public TextMeshProUGUI finalScoreText;

    [Tooltip("Text element displaying the high score on the Game Over screen.")]
    public TextMeshProUGUI highScoreText;

    [Header("State Flags")]
    public bool isPlaying = false;
    public bool isGameOver = false;

    private int score = 0;
    private int highScore = 0;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (startScreen != null) startScreen.SetActive(true);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (scoreText != null) scoreText.gameObject.SetActive(false);

        Time.timeScale = 1f;
        SetWorldScrolling(false);
    }

    void Update()
    {

        if (!isPlaying && !isGameOver)
        {
            bool pointerPressed = Pointer.current != null && Pointer.current.press.wasPressedThisFrame;
            bool spacePressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;

            if (pointerPressed || spacePressed)
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        isPlaying = true;
        isGameOver = false;
        score = 0;

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = "0";
        }

        if (startScreen != null) startScreen.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);

        SetWorldScrolling(true);

        PipeSpawner spawner = FindAnyObjectByType<PipeSpawner>();
        if (spawner != null)
        {
            spawner.ResetSpawner();
        }
    }

    public void AddScore(int amount = 1)
    {
        score += amount;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void GameOver()
    {
        isPlaying = false;
        isGameOver = true;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + score;
        }
        if (highScoreText != null)
        {
            highScoreText.text = "Best: " + highScore;
        }

        SetWorldScrolling(false);
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetWorldScrolling(bool scroll)
    {

        ParallaxScroll[] backgroundLayers = FindObjectsByType<ParallaxScroll>(FindObjectsSortMode.None);
        foreach (ParallaxScroll layer in backgroundLayers)
        {
            layer.SetScrolling(scroll);
        }
    }
}
