using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float speed = 15f;
    public int damage = 1; // Matches 'damageToDeal' in your Croc script
    public Rigidbody2D rb;

    void Start()
    {
        // 1. Movement: Fly forward (using the rotation logic we fixed earlier)
        rb.gravityScale = 0; 
        rb.linearVelocity = transform.right * speed; 
        
        // 2. Cleanup: Destroy bullet after 2 seconds so it doesn't float forever
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Debugging: What did we just hit?
        // Debug.Log("Bullet hit: " + hitInfo.name);

        // Ignore collision with the Player
        if (hitInfo.CompareTag("Player")) return;

        // 1. Try to find the Boss Script
        CrocodilePatrol boss = hitInfo.GetComponent<CrocodilePatrol>();

        // 2. Fallback: Sometimes the collider is on a child object (like the head)
        // so we check the parent too.
        if (boss == null)
        {
            boss = hitInfo.GetComponentInParent<CrocodilePatrol>();
        }
        
        // 3. If we found the boss, deal damage!
        if (boss != null)
        {
            boss.TakeDamage(damage); 
            Destroy(gameObject); // Destroy the bullet
        }
        else if (hitInfo.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject); // Destroy bullet if it hits a wall/floor
        }
    }
}