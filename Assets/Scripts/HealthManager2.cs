using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    // This function detects when something enters the Crocodile's space
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that stepped on the croc is tagged "Deer"
        if (other.CompareTag("Player"))
        {
            TakeDamage(10f); // Adjust damage amount here
            Debug.Log("The Deer stepped on the Croc!");
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Crocodile has fainted.");
        // Add death animation or logic here
    }
}