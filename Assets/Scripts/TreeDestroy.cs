using UnityEngine;

public class TreeDestroy : MonoBehaviour
{
    // Drag your Crocodile (Boss) GameObject here in the Inspector
    public GameObject boss; 

    void Update()
    {
        // Check if the boss object has been destroyed (is null)
        if (boss == null)
        {
            // If the boss is gone, destroy this tree so the player can pass
            Destroy(gameObject);
            
            // Optional: You could play a sound effect or particle effect here before destroying
            // e.g., AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }
    }
}