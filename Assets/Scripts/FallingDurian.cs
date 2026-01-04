using UnityEngine;

public class FallingDurian : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. If it hits the Deer -> Ouch
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("💥 BONK! Durian hit the deer!");
            
            // Trigger the Game Over screen
            if (GameManagerReworked.Instance != null)
            {
            GameManagerReworked.Instance.TriggerGameOver("durian");            
            }
        }

        // 2. Destroy the Durian shortly after hitting ANYTHING
        Destroy(gameObject, 3f);
    }

    
}