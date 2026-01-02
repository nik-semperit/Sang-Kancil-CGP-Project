using UnityEngine;

public class ParrotNPC : MonoBehaviour
{
    [Header("References")]
    public ChoiceManager choiceManager; // Drag ChoiceBox here
    public GameObject interactPrompt;   // Drag "Press F" Text here
    public GameObject player;           // Drag Deer here

    private bool playerInRange;

    void Update()
    {
        // Check if Player is close AND presses F
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // 1. Hide the "Press F" prompt so it's not ugly
            interactPrompt.SetActive(false);

            // 2. Open the menu with Debug functions attached
            choiceManager.StartChoice(
                "Squawk! I can grant you one blessing. Choose wisely!", // Question
                "Speed Buff (+Move)",    // Option A Label
                "Health Buff (+1 HP)",   // Option B Label
                GiveSpeedBuff,           // Action A
                GiveHealthBuff           // Action B
            );
        }
    }

    // --- THE BUFF FUNCTIONS (With Debugs) ---
    void GiveSpeedBuff()
    {
        // Check your Console (Bottom Left) for this message!
        Debug.Log("⚡ SPEED BUFF APPLIED! (Code is working)");
        
        // TODO: Add actual speed logic here later
        // Example: player.GetComponent<PlayerMovement>().speed += 5;
    }

    void GiveHealthBuff()
    {
        // Check your Console (Bottom Left) for this message!
        Debug.Log("❤️ HEALTH BUFF APPLIED! (Code is working)");

        // TODO: Add actual health logic here later
        // Example: player.GetComponent<Health>().Heal(1);
    }

    // --- PROXIMITY LOGIC ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactPrompt.SetActive(true); // Show "Press F"
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactPrompt.SetActive(false); // Hide "Press F"
        }
    }
}