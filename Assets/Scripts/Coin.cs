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
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(scoreValue);
            }
            else
            {
                Debug.LogWarning("No ScoreManager found in the scene!");
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