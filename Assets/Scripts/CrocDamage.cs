using UnityEngine;

public class CrocDamage : MonoBehaviour
{
    public int damageToPlayer = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DeerHealth playerHealth =
                collision.gameObject.GetComponent<DeerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
        }
    }
}
