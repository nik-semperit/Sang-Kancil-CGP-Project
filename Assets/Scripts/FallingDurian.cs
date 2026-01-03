using UnityEngine;

public class FallingDurian : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. If it hits the Deer -> Ouch
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("💥 BONK! Durian hit the deer!");
            
            // If you have a health script, call it here:
            // collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }

        // 2. Destroy the Durian shortly after hitting ANYTHING (Ground or Player)
        // The '1f' means wait 1 second (so it rolls a bit) then delete.
        Destroy(gameObject, 1f);
    }
}