using UnityEngine;

public class CrocMouthKill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManagerReworked.Instance != null)
            {
                GameManagerReworked.Instance.TriggerGameOver("crocodile");
            }
        }
    }
}
