using UnityEngine;

public class WaterHazard : MonoBehaviour
{
    // We removed the "public variable" so you don't have to drag anything manually.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the Singleton directly
            if (GameManagerReworked.Instance != null)
            {
                GameManagerReworked.Instance.TriggerGameOver();
            }
        }
    }
}