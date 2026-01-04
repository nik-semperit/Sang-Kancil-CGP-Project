using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;
    public AudioClip collectSound;
    public GameObject collectEffect; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Use the NEW class name "GameManagerReworked"
            if (GameManagerReworked.Instance != null)
            {
                GameManagerReworked.Instance.AddScore(scoreValue);
            }
            else
            {
                Debug.LogWarning("No GameManagerReworked found! Score not added.");
            }

            // Play Sound
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            
            // Spawn Particles
            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }
}