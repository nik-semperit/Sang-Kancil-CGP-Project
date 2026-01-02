using UnityEngine;

public class MovingPlatform2S : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDisable()
    {
        // 🔥 SAFETY: unparent player before croc disappears
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                child.SetParent(null);
            }
        }
    }
}
