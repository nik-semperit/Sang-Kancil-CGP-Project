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

    void Awake()
    {
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

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        
        if (gameOverCanvasPrefab != null)
        {
            Instantiate(gameOverCanvasPrefab);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}