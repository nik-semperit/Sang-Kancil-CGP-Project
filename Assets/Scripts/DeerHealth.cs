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

    void Die()
    {
        isDead = true;

        if (GameManagerReworked.Instance != null)
        {
            GameManagerReworked.Instance.TriggerGameOver("Player");
        }

        Destroy(gameObject);
    }
}
