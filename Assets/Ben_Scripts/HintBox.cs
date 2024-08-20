using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HintBox : MonoBehaviour, IPointerClickHandler
{
    public string[] dialogueLines; // The lines of dialogue
    public float typingSpeed = 0.05f; // Speed of the typing effect

    public GameObject dialogueUI; // Reference to the hintbox ui gameobject
    //public Image textboxImage; // Reference to the Image component for the textbox
    public Text dialogueText; // Reference to the Text component for the dialogue
    
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool isDialogueActive = false;

    private void Start()
    {

        dialogueUI.SetActive(false); // Hide the UI at the start
    }

    private void Update()
    {

        if (isDialogueActive)
        {

                DisplayNextLine();
        }
    }
    //just change this to begin dailogue ollie

    public void OnPointerClick(PointerEventData eventData)
    {
            StartDialogue();
             dialogueUI.SetActive(true);
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        currentLineIndex = 0;

        // Start the camera zoom-in effect
        /*StartCoroutine(CameraZoomIn());*/
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void DisplayNextLine()
    {
        if (isTyping) return; // Prevent skipping while typing

        if (currentLineIndex < dialogueLines.Length - 1)
        {
            currentLineIndex++;
            StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        // Hide the UI before zooming out
        dialogueUI.SetActive(false);

        // Start the camera zoom-out effect


        isDialogueActive = false;
    }

    
}

