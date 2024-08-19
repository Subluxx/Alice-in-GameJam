using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour, IPointerClickHandler
{
    public string[] dialogueLines; // The lines of dialogue
    public float interactionRange = 5f; // Range within which the player can interact with the NPC
    public float typingSpeed = 0.05f; // Speed of the typing effect

    public GameObject dialogueUI; // Reference to the dialogue UI GameObject
    public Image textboxImage; // Reference to the Image component for the textbox
    public Text dialogueText; // Reference to the Text component for the dialogue
    public Transform npcTransform; // Reference to the NPC's transform
    public Transform originalCameraPosition;
    
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool isDialogueActive = false;

    private Camera mainCamera;
    private float originalCameraSize;
    private bool isCameraZoomed = false;

    private GameObject player;
    private CharacterMovement playerMovement;
    private bool isPlayerInRange = false;

    private void Start()
    {
        mainCamera = Camera.main;
        originalCameraSize = mainCamera.orthographicSize;

        dialogueUI.SetActive(false); // Hide the UI at the start

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<CharacterMovement>();
        }
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            isPlayerInRange = distanceToPlayer <= interactionRange;
        }

        if (isDialogueActive)
        {
            // Position the UI above the NPC
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(npcTransform.position);
            dialogueUI.transform.position = screenPosition + new Vector3(0, 100, 0); 

            if (Input.GetMouseButtonDown(0))
            {
                DisplayNextLine();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlayerInRange && !isDialogueActive)
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        currentLineIndex = 0;

        // Lock player controls
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Start the camera zoom-in effect
        StartCoroutine(CameraZoomIn());
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
            StartCoroutine(EndDialogue());
        }
    }

    private IEnumerator EndDialogue()
    {
        // Hide the UI before zooming out
        dialogueUI.SetActive(false);

        // Start the camera zoom-out effect
        yield return StartCoroutine(CameraZoomOut());

        // Unlock player controls
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        isDialogueActive = false;
    }

    private IEnumerator CameraZoomIn()
    {
        Vector3 targetPosition = npcTransform.position;
        targetPosition.z -= 10;
        targetPosition.y += 3;

        float duration = 0.5f; // Duration of the zoom-in effect
        float elapsedTime = 0f;

        Vector3 startingPosition = mainCamera.transform.position;
        float startingSize = mainCamera.orthographicSize;
        float targetSize = originalCameraSize * 0.5f; 

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            mainCamera.orthographicSize = Mathf.Lerp(startingSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.orthographicSize = targetSize;
        isCameraZoomed = true;

        // Now that the camera is zoomed in, show the dialogue UI
        dialogueUI.SetActive(true);
        StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
    }

    private IEnumerator CameraZoomOut()
    {
        if (!isCameraZoomed) yield break;

        float duration = 0.5f; // Duration of the zoom-out effect
        float elapsedTime = 0f;

        Vector3 startingPosition = mainCamera.transform.position;
        float startingSize = mainCamera.orthographicSize;

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startingPosition, originalCameraPosition.position, elapsedTime / duration);
            mainCamera.orthographicSize = Mathf.Lerp(startingSize, originalCameraSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalCameraPosition.position;
        mainCamera.orthographicSize = originalCameraSize;
        isCameraZoomed = false;
    }
}
