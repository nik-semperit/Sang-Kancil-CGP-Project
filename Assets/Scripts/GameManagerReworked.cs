using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;
using TMPro;

// The class name MUST match the file name "GameManagerReworked"
public class GameManagerReworked : MonoBehaviour
{
    public static GameManagerReworked Instance; // Update this type too!
    

    [Header("UI Reference")]
    public GameObject gameOverCanvasPrefab;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    private bool isGameOver = false;
    [Header("UI References")]
    public GameObject pauseOverlayPanel; // The dark screen with menu options
    public GameObject hudPanel;          // The normal game UI (optional, if you want to hide it)
    public GameObject btnPlay;

    private bool isPaused = false;

    void Update()
    {
        // Keyboard Shortcut
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameOver) return; 
            TogglePause();
        }
    }

    // Connect this function to your HUD Pause Button AND Resume Button
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // PAUSE MODE
            pauseOverlayPanel.SetActive(true); // Show dark screen
            Time.timeScale = 0f;       // ZA WARUDO
            btnPlay.SetActive(true);         // Set button play to true
            
            // Optional: Hide the HUD button so it isn't clickable while paused
            // hudPanel.SetActive(false); 
        }
        else
        {
            // GAMEPLAY MODE
            pauseOverlayPanel.SetActive(false); // Hide dark screen
            Time.timeScale = 1f;                // Resume time
            
            // Optional: Show HUD again
            // hudPanel.SetActive(true);
        }
    }


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
    }

// --- NEW: SCORE FUNCTION ---
    public void AddScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        
        // Update the UI if we have it linked
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        Debug.Log("Coin Collected! New Score: " + score);
    }

    public void TriggerGameOver(string deathReason)
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverCanvasPrefab != null)
        {
            // 1. Spawn the screen
            GameObject goScreen = Instantiate(gameOverCanvasPrefab);

            // 2. Get our custom "Tag" script
            GameOverScreen screenScript = goScreen.GetComponent<GameOverScreen>();

            // 3. Define the message
            string message = "";
            switch (deathReason)
            {
                case "water": message = "You drowned in the river!"; break;
                case "durian": message = "Bonked by a Durian!"; break;
                case "crocodile": message = "Snap! A crocodile got you."; break;
                default: message = "Game Over!"; break;
            }

            // 4. Update the text safely
            if (screenScript != null && screenScript.reasonText != null)
            {
                screenScript.reasonText.text = message;
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}