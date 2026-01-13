using UnityEngine;

public class waterdamage2 : MonoBehaviour
{
    public int damage = 20;
    public float damageInterval = 1f; // damage every 1 second

    private float damageTimer = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        DeerHealth playerHealth = other.GetComponent<DeerHealth>();
        if (playerHealth == null) return;

        damageTimer += Time.deltaTime;

        if (damageTimer >= damageInterval)
        {
            playerHealth.TakeDamage(damage);
            damageTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer = 0f; // reset when leaving water
        }
    }
}
