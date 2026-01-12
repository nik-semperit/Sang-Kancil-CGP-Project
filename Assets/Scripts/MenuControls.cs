using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    public void RestartGame()
    {
        // This ensures the game isn't frozen when you restart
        Time.timeScale = 1f;

        // This reloads the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
