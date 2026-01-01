using UnityEngine;

public class StompTrigger : MonoBehaviour
{
    // Reference to the Croc script (on the parent object)
    private CrocodilePatrol crocScript;

    void Start()
    {
        crocScript = GetComponentInParent<CrocodilePatrol>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Check if it's the Player
        if (other.CompareTag("Player"))
        {
            // 2. Check if the player is falling DOWN (so they don't trigger it jumping up)
            if (other.attachedRigidbody.linearVelocity.y < 0)
            {
                // ACTION A: Hurt the Croc
                crocScript.TakeDamage(1);

                // ACTION B: Propel the Deer Upwards
                DeerMovement deer = other.GetComponent<DeerMovement>();
                if (deer != null)
                {
                    deer.Bounce();
                }
            }
        }
    }
}