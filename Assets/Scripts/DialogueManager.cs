using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    
    // Settings
    public float typingSpeed = 0.05f; // Lower = Faster

    private Queue<string> sentences;
    private string currentSentence;   // Store current sentence for skipping
    private bool isTyping = false;    // Are we currently animating?

    public static bool isTalking = false;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(string[] lines)
    {
        isTalking = true;
        dialogueBox.SetActive(true);
        sentences.Clear();

        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // 1. If we are currently typing, the player pressed the button early.
        //    Stop typing and show the full text instantly (The "Skip" feature).
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        // 2. If no more sentences, end.
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // 3. Start typing the next sentence
        currentSentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(currentSentence));
    }

    // This is the Magic Function
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = ""; // Clear text box

        // Loop through every letter in the sentence
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; // Add one letter
            
            // Optional: Play a sound effect here for the "Beep"
            // AudioManager.instance.Play("VoiceBeep"); 

            // Wait for a tiny fraction of a second
            yield return new WaitForSeconds(typingSpeed); 
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueBox.SetActive(false);
    }
}