using UnityEngine;
using UnityEngine.SceneManagement; // Needed to change levels
using TMPro; // Add this
using System.Collections; // Add this for the timer

public class LevelGoal : MonoBehaviour
{
    public int requiredScore = 10;       // The minimum points needed
    public string nextLevelName;       // The name of your next scene

    public GameObject warningUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object touching the goal is the Player
        if (other.CompareTag("Player"))
        {
            // Check the ScoreManager instance we just created
            if (ScoreManager.instance.score >= requiredScore)
            {
                Debug.Log("Success! Loading next level.");
                SceneManager.LoadScene(nextLevelName);
            }
            else
            {
                int missing = requiredScore - ScoreManager.instance.score;
                Debug.Log("Locked! You still need " + missing + " more fruits.");
            }
        }
    }
    IEnumerator ShowWarning()
    {
        warningUI.SetActive(true);
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        warningUI.SetActive(false);
    }
}