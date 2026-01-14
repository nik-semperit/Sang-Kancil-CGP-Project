using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class FlowerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Death Effect")]
    public GameObject deathEffect;

    public HealthBar healthBar;   // your slider-based health bar
    private bool isDead = false;

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
        if (isDead) return;

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
        isDead = true;

        // 🌸 Spawn death particle
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

}
