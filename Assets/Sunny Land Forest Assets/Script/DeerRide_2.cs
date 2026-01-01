using UnityEngine;

public class DeerRide_2 : MonoBehaviour
{
    private PlayerMovement_2 player;
    private EnemyMovement_2 croc;   // The crocodile currently under the deer

    private void Awake()
    {
        // Get the movement script from the main Deer object
        player = GetComponentInParent<PlayerMovement_2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Make sure this trigger belongs to a crocodile
        if (collision.CompareTag("Croc"))
        {
            // Find the crocodile movement script on the parent object
            croc = collision.GetComponentInParent<EnemyMovement_2>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Croc"))
        {
            croc = null;
        }
    }

    private void LateUpdate()
    {
        // If deer is NOT on a crocodile → stop here
        if (croc == null)
            return;

        // 1. Move deer with crocodile movement speed
        player.rb.linearVelocity = new Vector2(
            player.rb.linearVelocity.x + croc.CurrentSpeed,
            player.rb.linearVelocity.y
        );

        // 2. Slip-off check (if deer walks too far from the croc’s centre)
        float distance = Mathf.Abs(transform.position.x - croc.transform.position.x);

        if (distance > 0.7f)
        {
            croc = null; // slip off
        }
    }
}

