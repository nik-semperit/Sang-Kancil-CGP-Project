using UnityEngine;

public class BushHideTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        DeerMovement deer = other.GetComponent<DeerMovement>();
        if (deer != null)
        {
            deer.isHiddenInBush = true;

            // Ignore croc collisions
            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Player"),
                LayerMask.NameToLayer("Enemies"),
                true
            );
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        DeerMovement deer = other.GetComponent<DeerMovement>();
        if (deer != null)
        {
            deer.isHiddenInBush = false;

            // Restore collisions
            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Player"),
                LayerMask.NameToLayer("Enemies"),
                false
            );
        }
    }
}
