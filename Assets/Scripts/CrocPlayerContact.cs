using UnityEngine;

public class CrocPlayerContact : MonoBehaviour
{
    CrocWalk walk;

    void Start()
    {
        walk = GetComponent<CrocWalk>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Player standing on top
            if (contact.normal.y > 0.5f)
            {
                walk.PauseMovement(true);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            walk.PauseMovement(false);
        }
    }
}
