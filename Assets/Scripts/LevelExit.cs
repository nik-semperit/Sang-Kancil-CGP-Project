using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip exitSound;

    private bool levelLoading = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelLoading)
        {
            levelLoading = true;
            
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
        Time.timeScale = 1f;

        // Get the current index (e.g., 1) and add 1 to get the next one (e.g., 2)
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // SAFETY CHECK: Does the next scene actually exist?
        // If we are at the last level, this prevents a crash by going back to Menu (0)
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels! looping back to Main Menu.");
            SceneManager.LoadScene(0); 
        }
    }
}