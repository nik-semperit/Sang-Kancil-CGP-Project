using UnityEngine;
using UnityEngine.SceneManagement; // Needed for the Menu button

public class GameOverUI : MonoBehaviour
{
    // Function for "Try Again" button
    public void ClickRestart()
    {
        // Calls the Restart function we made in the Manager
        if (GameManagerReworked.Instance != null)
        {
            GameManagerReworked.Instance.RestartLevel();
        }
    }

    // Function for "Main Menu" button
    public void ClickMainMenu()
    {
        // Ensure time is running again before leaving
        Time.timeScale = 1; 
        
        // IMPORTANT: You must have a scene named "MainMenu" in your Build Settings!
        SceneManager.LoadScene("MainMenu"); 
    }
}