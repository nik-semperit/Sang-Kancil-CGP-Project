using UnityEngine;

public class PlayerProjectileProjectile : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 1;
    public Rigidbody2D rb; // Drag the Rigidbody2D component here in Inspector

    void Start()
    {
        // Takes the rotation from the FirePoint and flies "Right" relative to it
        rb.linearVelocity = transform.right * speed;
        
        // Destroy bullet after 2 seconds so it doesn't float forever and lag the game
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if we hit an enemy
        //Enemy enemy = hitInfo.GetComponent<Enemy>();
        //if (enemy != null)
        //{
          //  enemy.TakeDamage(damage); // We will write the Enemy script later if you don't have it
          //  Destroy(gameObject); // Destroy Bullet
        //}
        
        // Also destroy bullet if it hits the ground/walls
        // Make sure your Ground has a "Ground" Layer or Tag if you want this precise
        if (hitInfo.CompareTag("Ground")) 
        {
            Destroy(gameObject);
        }
    }
}