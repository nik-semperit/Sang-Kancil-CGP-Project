using UnityEngine;

public class TripleJumpPotion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Get the specific script attached to the Deer that hit us
            DeerMovement deer = other.GetComponent<DeerMovement>();

            if (deer != null)
            {
                // 2. Change the variable on THAT deer
                // Current is 1 (Double Jump). Set to 2 for Triple Jump.
                deer.extraJumpsValue = 2; 

                Debug.Log("🦅 TRIPLE JUMP UNLOCKED! (Extra Jumps: " + deer.extraJumpsValue + ")");

                // 3. Destroy the potion
                Destroy(gameObject);
            }
        }
    }
}