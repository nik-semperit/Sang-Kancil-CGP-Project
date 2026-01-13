using UnityEngine;

public class CrocMouthDamage : MonoBehaviour
{
    public int damage = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        DeerMovement deer = other.GetComponent<DeerMovement>();

        // 🔥 ONLY ignore if hiding (only bush stages set this)
        if (deer != null && deer.isHiddenInBush)
            return;

        DeerHealth playerHealth = other.GetComponent<DeerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
