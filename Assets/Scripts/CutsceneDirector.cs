using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // <--- VITAL: Allows us to control UI Images


public class CutsceneDirector : MonoBehaviour
{

    [Header("UI")]
    public Image fadePanel; // <--- Drag your Black Panel here
    public float fadeSpeed = 1.0f;

    [Header("Actors")]
    public CutsceneActor deer;
    
    [Header("Pacing Settings (Walking Back & Forth)")]
    public Transform pacePointA;    // Left side of patrol
    public Transform pacePointB;    // Right side of patrol
    public int numberOfLaps = 2;    // How many times to walk back and forth?
    
    [Header("The Escape")]
    public Transform separateSpot;  // Point B (Where he runs to before quake)

    [Header("The Earthquake")]
    public CameraShake cameraShaker;
    public Transform brokenLandPiece;
    public float landDriftSpeed = 1.5f; // HOW FAST it moves
    public float driftDuration = 5.0f;  // HOW LONG it moves (Increase this to go further!)

    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    
    {
        // --- PHASE 1: PACING (Walking Back and Forth) ---
        Debug.Log("Scene Start: Deer is pacing...");

        // Loop for however many 'Laps' you set
        for (int i = 0; i < numberOfLaps; i++)
        {
            // Walk to Point A
            yield return StartCoroutine(WalkToTarget(pacePointA));
            yield return new WaitForSeconds(1f); // Wait 1 sec

            // Walk to Point B
            yield return StartCoroutine(WalkToTarget(pacePointB));
            yield return new WaitForSeconds(1f); // Wait 1 sec
        }

        // --- PHASE 2: MOVE TO SEPARATION SPOT ---
        Debug.Log("Deer hears something and moves to edge...");
        yield return StartCoroutine(WalkToTarget(separateSpot));

        // Turn around to look back at family (Flip X)
        // We multiply current X by -1 to flip to the opposite side
        float currentFacing = deer.transform.localScale.x;
        deer.transform.localScale = new Vector3(-currentFacing, 1, 1); 
        
        yield return new WaitForSeconds(1.0f); // Dramatic pause

        // --- PHASE 3: THE EARTHQUAKE & SPLIT ---
        Debug.Log("RUMBLE!");
        
        if (cameraShaker != null) cameraShaker.TriggerShake();
        
        // Move the Land (The Split!)
        float splitTime = 0f;
        while (splitTime < driftDuration) // <--- USES YOUR NEW DURATION VARIABLE
        {
            if (brokenLandPiece != null)
            {
                // Move land
                brokenLandPiece.Translate(Vector3.right * landDriftSpeed * Time.deltaTime);
                // Move deer with land
                deer.transform.Translate(Vector3.right * landDriftSpeed * Time.deltaTime);
            }
            
            splitTime += Time.deltaTime;
            yield return null;
        }

        // --- PHASE 4: END ---
        Debug.Log("Oh no! Separated!");
        yield return new WaitForSeconds(2.0f);
     
     // --- PHASE 4: FADE OUT ---
        Debug.Log("Fading out...");
        
        // This block slowly turns the alpha from 0 to 1
        if (fadePanel != null)
        {
            float alpha = 0f;
            while (alpha < 1f)
            {
                alpha += Time.deltaTime * fadeSpeed;
                // Update the color with the new alpha
                fadePanel.color = new Color(0, 0, 0, alpha); 
                yield return null;
            }
        }
        
        // Wait one extra split second for dramatic effect
        yield return new WaitForSeconds(0.1f);

        // NOW we load the level
        SceneManager.LoadScene("Level1_Viknes");

    }

    // --- HELPER FUNCTION: Makes walking cleaner ---
    // This handles the walking logic so we don't have to copy-paste it 3 times
    IEnumerator WalkToTarget(Transform target)
    {
        // 1. Face the Direction
        if (target.position.x > deer.transform.position.x)
            deer.transform.localScale = new Vector3(-1, 1, 1); // Face Right
        else 
            deer.transform.localScale = new Vector3(1, 1, 1); // Face Left

        // 2. Start Animation
        deer.anim.SetFloat("Speed", 3f); 

        // 3. Move until close
        while (Vector3.Distance(deer.transform.position, target.position) > 0.1f)
        {
            deer.transform.position = Vector3.MoveTowards(deer.transform.position, target.position, 3f * Time.deltaTime);
            yield return null;
        }

        // 4. Stop Animation
        deer.anim.SetFloat("Speed", 0f);
    }


    
}