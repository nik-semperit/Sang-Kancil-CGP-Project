
using UnityEngine;

public class CrocHealth2 : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar; // your slider-based UI

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // For now just disable croc
        gameObject.SetActive(false);
    }
}
