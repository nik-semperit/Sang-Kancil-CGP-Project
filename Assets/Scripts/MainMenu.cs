using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for Coroutines

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject mainButtons;   // Play & Credits Group
    public GameObject backButton;    // The Return Button
    public MenuCamera camScript;     // The Camera
    
    [Header("Settings")]
    public float delayTime = 0.8f;   // How long to wait before showing the new button

    // ---------------------------------------------------------
    // 1. The Play Button
    // ---------------------------------------------------------
    public void PlayGame()
    {
        // Loads the next scene (Index 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting..."); 
        Application.Quit();
    }

    // ---------------------------------------------------------
    // 2. Go To Credits (With Delay)
    // ---------------------------------------------------------
    public void GoToCredits()
    {
        // Use a Coroutine to handle the timing
        StartCoroutine(CreditsSequence());
    }

    IEnumerator CreditsSequence()
    {
        // Step A: Hide main buttons immediately (so players don't click twice)
        if(mainButtons != null) mainButtons.SetActive(false);

        // Step B: Start the camera movement
        camScript.MoveToCredits();

        // Step C: WAIT! (Let the camera fly for a bit)
        yield return new WaitForSeconds(delayTime);

        // Step D: Show the Back button (Now that we have arrived)
        if(backButton != null) backButton.SetActive(true);
    }

    // ---------------------------------------------------------
    // 3. Go Back (With Delay)
    // ---------------------------------------------------------
    public void GoBack()
    {
        StartCoroutine(BackSequence());
    }

    IEnumerator BackSequence()
    {
        // Step A: Hide back button immediately
        if(backButton != null) backButton.SetActive(false);

        // Step B: Start camera movement
        camScript.MoveToMain();

        // Step C: WAIT!
        yield return new WaitForSeconds(delayTime);

        // Step D: Show Main Menu again
        if(mainButtons != null) mainButtons.SetActive(true);
    }
}