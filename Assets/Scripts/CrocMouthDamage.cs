using UnityEngine;

public class CrocMouthDamage : MonoBehaviour
{
    public int damage = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeerHealth playerHealth = other.GetComponent<DeerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
