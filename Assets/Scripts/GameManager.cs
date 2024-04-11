using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int scoreCount = 0;
    public bool isGameActive;
    public GameObject gameOverPanel;
    public Text scoreCountText;
    public GameObject settingsPanel;
    public bool isGamePaused = false;
    public GameObject PauseButton;
    public GameObject scoreText;
    public GameObject levelCompletePanel;
    public AudioSource audioSource;
    public int highScoreCount = 0;
    public Text highScoreText;
    public GameObject adsManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        highScoreCount = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        isGameActive = true;
        Time.timeScale = 1;
        UpdateScoreCountText();
        UpdateHighScoreText();
    }

    public void Gainscore(int amount)
    {
        if (isGameActive)
        {
            scoreCount += amount;
            UpdateScoreCountText();

            if (scoreCount > highScoreCount)
            {
                highScoreCount = scoreCount;
                PlayerPrefs.SetInt("HighScore", highScoreCount); 
                UpdateHighScoreText();
            }
        }
    }

    private void UpdateScoreCountText()
    {
        if (scoreCountText != null)
        {
            scoreCountText.text = "" + scoreCount;
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "" + highScoreCount;
        }
    }
    

    public void ToggleSettingsPanel()
    {
        isGamePaused = !isGamePaused;
        settingsPanel.SetActive(isGamePaused);
        Time.timeScale = isGamePaused ? 0 : 1; 
    }

    

    public void LevelComplete()
    {
        Debug.Log("Level Complete!");
        levelCompletePanel.SetActive(true);
        isGameActive = false;
        PauseButton.SetActive(false);
        scoreText.SetActive(false);
        Time.timeScale = 0;
    }


    public void GameOver()
    {   
        
        if (isGameActive)
        {
            isGameActive = false;
            gameOverPanel.SetActive(true);
            PauseButton.SetActive(false);
            scoreText.SetActive(false);
            Time.timeScale = 0;

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
            if(adsManager != null)
            {
                FindObjectOfType<Interstitial>().ShowAd();
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
