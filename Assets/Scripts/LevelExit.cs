using UnityEngine;
using UnityEngine.SceneManagement; // Needed to change levels

public class LevelExit : MonoBehaviour
{
    [Header("Settings")]
    public string nextLevelName; // Type the EXACT name of your next scene here (e.g., "Level2")
    
    [Header("Audio (Optional for Rubric)")]
    public AudioSource audioSource;
    public AudioClip exitSound;

    private bool levelLoading = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only the player can trigger the level change
        if (other.CompareTag("Player") && !levelLoading)
        {
            levelLoading = true; // Prevent double triggering
            Debug.Log("Exiting Level...");
            
            // Play sound if you have one
            if (audioSource != null && exitSound != null)
            {
                AudioSource.PlayClipAtPoint(exitSound, transform.position);
            }

            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        // CRITICAL: Ensure time is running (in case you were paused)
        Time.timeScale = 1f;
        
        // Load the scene you typed in the Inspector
        SceneManager.LoadScene(nextLevelName);
    }
}