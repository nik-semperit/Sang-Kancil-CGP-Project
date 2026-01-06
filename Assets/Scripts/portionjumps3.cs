using UnityEngine;

public class portionjumps3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeerMovement deer = other.GetComponent<DeerMovement>();

            if (deer != null)
            {
                deer.extraJumpsValue = 2;       // TEMP TRIPLE JUMP
                deer.oneTimeTripleJump = true; // ONE-TIME ONLY

                Destroy(gameObject);
            }
        }
    }
}
