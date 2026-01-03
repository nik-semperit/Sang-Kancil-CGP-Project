using UnityEngine;

public class ParrotNPC : MonoBehaviour
{
    [Header("References")]
    public ChoiceManager choiceManager; // Drag ChoiceBox here
    public GameObject interactPrompt;   // Drag "Press F" Text here
    public GameObject player;           // Drag Deer here

    [Header("Rewards")]
    public GameObject potionPrefab;    // Drag your Health Potion Prefab here
    public Transform rewardSpawnPoint; // Drag an empty object where you want it to drop

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
                GiveJumpBuff           // Action B
            );
        }
    }

    // --- THE BUFF FUNCTIONS (With Debugs) ---
    void GiveSpeedBuff()
    {
        // 1. Find the script on the player
        // CHANGE "PlayerMovement" to the actual name of your movement script file
        var movement = player.GetComponent<DeerMovement>();

        if (movement != null)
        {
            // 2. Add speed
            // CHANGE "moveSpeed" to the variable name you use for speed inside that script
            movement.moveSpeed += 5f; 

            Debug.Log("⚡ SPEED BUFF APPLIED! New Speed: " + movement.moveSpeed);
        }
        else
        {
            Debug.LogError("Check the code! I couldn't find the PlayerMovement script on the Deer.");
        }
    }

    void GiveJumpBuff()
    {
        // Check if we forgot to link the prefab
        if (potionPrefab != null && rewardSpawnPoint != null)
        {
            // Spawn the potion at the specific point, with no rotation
            Instantiate(potionPrefab, rewardSpawnPoint.position, Quaternion.identity);
            
            Debug.Log("❤️ JUMP POTION SPAWNED! Go pick it up.");
        }
        else
        {
            Debug.LogError("Error: You forgot to drag the Potion Prefab or Spawn Point into the Parrot Script!");
        }
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