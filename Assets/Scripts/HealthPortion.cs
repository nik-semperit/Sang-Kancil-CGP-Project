using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeerHealth deerHealth = other.GetComponent<DeerHealth>();

            if (deerHealth != null)
            {
                deerHealth.HealToFull(); // 💚 Refill to 100%
            }

            Destroy(gameObject); // 🧪 Remove potion after use
        }
    }
}
