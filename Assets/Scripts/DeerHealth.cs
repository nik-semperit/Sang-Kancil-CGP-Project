using UnityEngine;

public class DeerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public PlayerHealthBar healthBar; // 🔥 UI reference

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
        isDead = true;

        if (CheckpointManager.Instance != null &&
            CheckpointManager.Instance.HasCheckpoint())
        {
            Respawn();
        }
        else
        {
            // No checkpoint → Game Over
            if (GameManagerReworked.Instance != null)
                GameManagerReworked.Instance.TriggerGameOver("Player");
        }
    }

    void Respawn()
    {
        currentHealth = maxHealth;
        isDead = false;

        transform.position = CheckpointManager.Instance.GetCheckpointPosition();

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

}
