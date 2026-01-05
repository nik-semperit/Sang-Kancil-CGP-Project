using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Drag your Deer GameObject here
    public float smoothSpeed = 0.125f; // Higher = faster lock, Lower = smoother
    public Vector3 offset;         // Default this to (0, 2, -10) in Inspector

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }

        if (target != null)
            transform.position = target.position + offset;
    }

    void LateUpdate()
    {
        // Only follow if the deer exists (hasn't fallen off world/died)
        if (target != null)
        {
            // 1. Calculate where the camera WANTS to be
            Vector3 desiredPosition = target.position + offset;

            // 2. Smoothly interpolate between current position and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 3. Update position
            transform.position = smoothedPosition;
        }
    }
}