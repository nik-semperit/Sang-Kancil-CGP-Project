using UnityEngine;

public class DeerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public PlayerHealthBar healthBar; // 🔥 UI reference

    [Header("Death Effect")]
    public GameObject deathEffect;
    public float respawnDelay = 0.8f;



    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void HealToFull()
    {
        if (isDead) return;

        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth); // 🔥 THIS WAS MISSING
    }


    void Die()
    {
        if (isDead) return;
        isDead = true;

        // 🔥 Spawn death particle
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Hide player temporarily
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().simulated = false;

        if (CheckpointManager.Instance != null &&
            CheckpointManager.Instance.HasCheckpoint())
        {
            Invoke(nameof(Respawn), respawnDelay);
        }
        else
        {
            if (GameManagerReworked.Instance != null)
                GameManagerReworked.Instance.TriggerGameOver("Player");
        }
    }


    void Respawn()
    {
        transform.position = CheckpointManager.Instance.GetCheckpointPosition();

        currentHealth = maxHealth;
        isDead = false;

        // Re-enable player
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }


}
