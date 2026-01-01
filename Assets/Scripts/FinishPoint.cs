using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    // Drag your "WinScreen" GameObject here in the Inspector
    public GameObject winScreen; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering is the Deer (Player)
        if (other.CompareTag("Player"))
        {
            Debug.Log("Working");
            WinGame();
        }
    }

    void WinGame()
    {
        // 1. Show the "You Won!" screen
        winScreen.SetActive(true);

        // 2. Stop the game time (Freezes the deer and enemies)
        Time.timeScale = 0f; 
        
        Debug.Log("Winner!");
    }
}