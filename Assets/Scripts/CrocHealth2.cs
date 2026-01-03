using UnityEngine;
using UnityEngine.UI;

public class CrocHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Image healthFill; // from UI kit

    void Start()
    {
        currentHealth = maxHealth;
        UpdateBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateBar();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateBar()
    {
        healthFill.fillAmount = (float)currentHealth / maxHealth;
    }
}
