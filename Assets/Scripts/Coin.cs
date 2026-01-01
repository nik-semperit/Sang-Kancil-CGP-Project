using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;
    public AudioClip collectSound;
    
    [Header("Effects")]
    public GameObject collectEffect; // <--- NEW VARIABLE

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Add Score
            FindObjectOfType<GameManager>().AddScore(scoreValue);

            // 2. Play Sound
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            
            // 3. SPAWN PARTICLES (New!)
            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }
            
            // 4. Destroy object
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }
}