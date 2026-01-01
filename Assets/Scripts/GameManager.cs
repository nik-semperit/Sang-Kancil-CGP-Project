using UnityEngine;
using UnityEngine.SceneManagement; // Required for restarting levels!
using UnityEngine.UI; // NEED THIS for Text
using TMPro; // Use this if you are using TextMeshPro (Recommended), otherwise delete


public class GameManager : MonoBehaviour
{

    public GameObject gameOverUI;
    
    [Header("Score System")]

    public TMP_Text scoreText;

    private int currentScore = 0;

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    // Call this when the player dies
    public void TriggerGameOver()
    {
        gameOverUI.SetActive(true); // Show the black screen
        Time.timeScale = 0f; // Freeze the game
    }

    // Call this from the "Restart" button
    public void RestartGame()
    {
        Time.timeScale = 1f; // Unfreeze time before reloading!
        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Call this from the "Quit" button
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    
}