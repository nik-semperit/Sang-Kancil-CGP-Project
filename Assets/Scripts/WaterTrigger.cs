using UnityEngine;

public class RiverTrigger : MonoBehaviour
{
    public CrocSpawner2 spawner;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            spawner.StartDifficultyRamp();
        }
    }
}
