using UnityEngine;
using TMPro;

public class BirdNPC : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI interactText;
    public GameObject dialogPanel;

    private bool playerInRange = false;
    private bool dialogOpen = false;

    void Start()
    {
        if (interactText != null)
            interactText.gameObject.SetActive(false);

        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleDialog();
        }
    }

    void ToggleDialog()
    {
        dialogOpen = !dialogOpen;

        if (dialogPanel != null)
            dialogPanel.SetActive(dialogOpen);

        if (interactText != null)
            interactText.gameObject.SetActive(!dialogOpen);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!dialogOpen && interactText != null)
                interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogOpen = false;

            if (interactText != null)
                interactText.gameObject.SetActive(false);

            if (dialogPanel != null)
                dialogPanel.SetActive(false);
        }
    }
}
