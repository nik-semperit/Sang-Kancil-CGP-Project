using UnityEngine;

// This allows you to right-click -> Create -> Dialogue -> New Conversation
[CreateAssetMenu(fileName = "NewConversation", menuName = "Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    // A list of lines to play in order
    public DialogueLine[] lines;
}

[System.Serializable]
public class DialogueLine
{
    [Header("Character Info")]
    public string speakerName;
    public Sprite portrait;     // The face of the character
    
    [Header("The Text")]
    [TextArea(3, 10)]           // Makes the text box bigger in Inspector
    public string text;

    [Header("Branching (Optional)")]
    // If this list is empty, the player just clicks to go to the next line.
    // If you add choices here, the game waits for the player to pick one.
    public Choice[] choices; 
}

[System.Serializable]
public class Choice
{
    public string buttonText;           // e.g., "I agree" or "I refuse"
    public Conversation nextConversation; // The new file to load if picked
}