using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 7f;
    public int damage = 1; // If you have health, otherwise it just restarts level
    public float lifeTime = 3f; // Disappear after 3 seconds

    void Start()
    {
        // Destroy the bullet automatically after 3 seconds so it doesn't clutter the game
        Destroy(gameObject, lifeTime);
        
        // Find the Rigidbody to move it
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        // Move straight based on the rotation we spawned it with
        // (transform.right is the Red Arrow / Forward direction)
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If it hits the Player
        if (other.CompareTag("Player"))
        {
            // Kill the player (Reuse your existing Game Over logic)
            FindObjectOfType<GameManager>().TriggerGameOver();
            
            // Destroy Bullet
            Destroy(gameObject);
        }
        // If it hits the Ground/Wall (Optional)
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}