using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [Header("Targets")]
    public Transform mainPos;    
    public Transform creditsPos; 

    [Header("Settings")]
    public float smoothSpeed = 5f; 

    private Vector3 targetPosition;

    void Start()
    {
        if (mainPos == null) Debug.LogError("❌ ERROR: Main Pos is empty on the Camera!");
        else 
        {
            targetPosition = mainPos.position;
            Debug.Log("✅ Camera initialized. Target set to Main: " + targetPosition);
        }
    }

    void Update()
    {
        // Move the camera
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

    public void MoveToCredits()
    {
        Debug.Log("📣 SIGNAL RECEIVED: MoveToCredits() was called!");
        
        if (creditsPos != null)
        {
            targetPosition = creditsPos.position;
            Debug.Log("🚀 New Target Set: " + targetPosition);
        }
        else
        {
            Debug.LogError("❌ ERROR: Credits Pos is MISSING inside the Camera inspector!");
        }
    }

    public void MoveToMain()
    {
        Debug.Log("📣 SIGNAL RECEIVED: MoveToMain() was called!");
        if (mainPos != null) targetPosition = mainPos.position;
    }
}