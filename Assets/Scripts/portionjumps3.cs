using UnityEngine;

public class potionjump3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeerMovement deer = other.GetComponent<DeerMovement>();

            if (deer != null)
            {
                deer.ActivateTripleJump();

                Debug.Log("🧪 Triple Jump READY!");

                Destroy(gameObject);
            }
        }
    }
}