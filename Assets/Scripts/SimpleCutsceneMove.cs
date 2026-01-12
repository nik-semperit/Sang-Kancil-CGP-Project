using UnityEngine;
using UnityEngine.SceneManagement; // Needed to load next level

public class SimpleCutsceneMove : MonoBehaviour
{
    [Header("Settings")]
    public Transform targetDestination; // Drag "PointB" here
    public float speed = 3f;
    public string nextLevelName = "Level 1"; // Exact name of your game scene

    [Header("Animation")]
    public Animator anim; // Drag your Animator here

    private bool hasArrived = false;

   void Update()
    {
        if (targetDestination == null) return;

        float distance = Vector3.Distance(transform.position, targetDestination.position);

        // --- NEW: FACE THE DIRECTION ---
        // Calculate the difference in X
        float directionX = targetDestination.position.x - transform.position.x;

        // If the target is to the right (positive), look right
        if (directionX > 0.1f) 
        {
            // NOTE: If your deer looks backwards, swap 1 and -1 here
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        // If the target is to the left (negative), look left
        else if (directionX < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // ---------------------------------

        if (distance > 0.1f)
        {
            // Move
            transform.position = Vector3.MoveTowards(transform.position, targetDestination.position, speed * Time.deltaTime);
            
            // Animate
            if (anim != null) anim.SetFloat("Speed", speed); 
        }
        else
        {
            // Stop
            if (!hasArrived)
            {
                hasArrived = true;
                if (anim != null) anim.SetFloat("Speed", 0f);
                Invoke("LoadNextLevel", 1f); 
            }
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}