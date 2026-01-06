using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;
using TMPro;

public class GameManagerReworked : MonoBehaviour
{
    public static GameManagerReworked Instance;

    // --- CHANGE 1: STATIC SCORE (Shared across all levels) ---
    public static int globalScore = 0; 

    [Header("UI Reference")]
    public GameObject gameOverCanvasPrefab;
    public TextMeshProUGUI scoreText; // Make sure your HUD Text is assigned here!

    private bool isGameOver = false;
    
    [Header("Pause Menu")]
    public GameObject pauseOverlayPanel; 
    public GameObject hudPanel;         
    public GameObject btnPlay;

    private bool isPaused = false;

    void Awake()
    {
        Time.timeScale = 1f; 
        isGameOver = false;

        // Singleton Setup
        if (Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        // --- CHANGE 2: Load the saved score immediately ---
        UpdateScoreUI();
    }

    void Update()
    {
        // Keyboard Shortcut for Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameOver) return; 
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            if(pauseOverlayPanel != null) pauseOverlayPanel.SetActive(true);
            Time.timeScale = 0f;      
            if(btnPlay != null) btnPlay.SetActive(true);
        }
        else
        {
            if(pauseOverlayPanel != null) pauseOverlayPanel.SetActive(false);
            Time.timeScale = 1f;             
        }
    }

    // --- CHANGE 3: UPDATED SCORE FUNCTION ---
    public void AddScore(int amount)
    {
        if (isGameOver) return;

        globalScore += amount; // Add to the GLOBAL bank
        UpdateScoreUI();       // Refresh the screen

        Debug.Log("Score Added! Total: " + globalScore);
    }

    // Helper function to update text
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + globalScore;
        }
    }

    // Call this if you want to reset score to 0 (e.g. on Game Over)
    public void ResetGlobalScore()
    {
        globalScore = 0;
        UpdateScoreUI();
    }

    public void TriggerGameOver(string deathReason)
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverCanvasPrefab != null)
        {
            GameObject goScreen = Instantiate(gameOverCanvasPrefab);
            GameOverScreen screenScript = goScreen.GetComponent<GameOverScreen>();

            string message = "";
            switch (deathReason)
            {
                case "water": message = "You drowned in the river!"; break;
                case "durian": message = "Bonked by a Durian!"; break;
                case "crocodile": message = "Snap! A crocodile got you."; break;
                default: message = "Game Over!"; break;
            }

            if (screenScript != null && screenScript.reasonText != null)
            {
                screenScript.reasonText.text = message;
            }
        }
    }

    public void RestartLevel()
    {
        // Optional: If you want score to reset on death, uncomment this:
        // ResetGlobalScore(); 
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}