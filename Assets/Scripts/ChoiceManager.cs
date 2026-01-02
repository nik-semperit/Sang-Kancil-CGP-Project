using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject choicePanel;      // Drag the Panel here
    public TextMeshProUGUI questionText; // Drag the Question Text here
    public Button buttonA;              // Drag Button A here
    public TextMeshProUGUI buttonAText; // Drag the Text inside Button A
    public Button buttonB;              // Drag Button B here
    public TextMeshProUGUI buttonBText; // Drag the Text inside Button B

    // This function acts as the "Setup" for the conversation
    // We use 'System.Action' to pass functions as arguments!
    public void StartChoice(string question, string optionA, string optionB, System.Action actionA, System.Action actionB)
    {
        // 1. Show the UI
        choicePanel.SetActive(true);
        Time.timeScale = 0; // Optional: Pause game while choosing

        // 2. Set the text
        questionText.text = question;
        buttonAText.text = optionA;
        buttonBText.text = optionB;

        // 3. Clear old button clicks (Important so they don't stack)
        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();

        // 4. Assign new functions to the buttons
        buttonA.onClick.AddListener(() => {
            actionA(); // Run the buff function
            ClosePanel();
        });

        buttonB.onClick.AddListener(() => {
            actionB(); // Run the buff function
            ClosePanel();
        });
    }

    void ClosePanel()
    {
        Time.timeScale = 1; // Unpause
        choicePanel.SetActive(false);
    }
}