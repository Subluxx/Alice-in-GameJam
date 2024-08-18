using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCDialogue : MonoBehaviour, IPointerClickHandler
{
    public string[] dialogueLines; // Array to hold the dialogue lines for the NPC
    public float interactionRange = 3f; // Range within which the player can interact with the NPC
    public float dialogueDisplayDuration = 5f; // How long each line of dialogue is displayed

    public Camera mainCamera; // Reference to the main camera
    public float cameraZoomDuration = 1f; // Duration of the camera zoom in/out
    public float cameraZoomFieldOfView = 30f; // The FOV when zoomed in
    public float focusPointDistance = 2f; // Distance in front of the NPC for the camera to focus on

    private bool isPlayerInRange = false;
    private bool isDialogueActive = false;
    private int currentLineIndex = 0;
    private GameObject player;
    private Coroutine dialogueCoroutine;

    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private float originalCameraFieldOfView;

    private Vector3 cameraFocusPoint; // Virtual focus point in front of the NPC

    private CharacterMovement playerMovement; // Reference to the player's movement script

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has the tag "Player"

        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Find the main camera if not assigned
        }

        // Get reference to the player's movement script
        playerMovement = player.GetComponent<CharacterMovement>();

        originalCameraPosition = mainCamera.transform.position;
        originalCameraRotation = mainCamera.transform.rotation;
        originalCameraFieldOfView = mainCamera.fieldOfView;

        // Calculate the focus point in front of the NPC
        cameraFocusPoint = transform.position + transform.forward * focusPointDistance;
        cameraFocusPoint.y += transform.position.y / 2;
    }

    private void Update()
    {
        // Check the distance between the NPC and the player
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            isPlayerInRange = distanceToPlayer <= interactionRange;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Only start dialogue if the player is within range and dialogue is not already active
        if (isPlayerInRange && !isDialogueActive)
        {
            StartDialogue();
        }
        else if (isDialogueActive)
        {
            // Finish the dialogue if it is currently active
            EndDialogue();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        currentLineIndex = 0;
        dialogueCoroutine = StartCoroutine(DisplayDialogue());

        // Lock player controls
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Start the camera zoom-in effect
        StartCoroutine(CameraZoomIn());
    }

    private IEnumerator DisplayDialogue()
    {
        while (currentLineIndex < dialogueLines.Length)
        {
            // Call your method here to display the dialogue line
            DisplayDialogueLine(dialogueLines[currentLineIndex]);

            // Wait for the player to click to advance the dialogue
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            currentLineIndex++;
        }

        EndDialogue();
    }

    private void EndDialogue()
    {
        isDialogueActive = false;

        // Stop the dialogue coroutine if it is still running
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        // Call your method here to hide the dialogue or cleanup
        HideDialogue();

        // Unlock player controls
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Start the camera zoom-out effect
        StartCoroutine(CameraZoomOut());
    }

    private void DisplayDialogueLine(string line)
    {
        // Implement your code here to display the dialogue line on the UI
        Debug.Log(line); // Placeholder for your actual dialogue display code
    }

    private void HideDialogue()
    {
        // Implement your code here to hide the dialogue
        Debug.Log("Dialogue ended."); // Placeholder for your actual dialogue hide code
    }

    private IEnumerator CameraZoomIn()
    {
        float elapsedTime = 0f;

        Vector3 targetPosition = cameraFocusPoint;
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);

        while (elapsedTime < cameraZoomDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(originalCameraPosition, targetPosition, elapsedTime / cameraZoomDuration);
            mainCamera.transform.rotation = Quaternion.Lerp(originalCameraRotation, targetRotation, elapsedTime / cameraZoomDuration);
            mainCamera.fieldOfView = Mathf.Lerp(originalCameraFieldOfView, cameraZoomFieldOfView, elapsedTime / cameraZoomDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and FOV are set correctly
        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
        mainCamera.fieldOfView = cameraZoomFieldOfView;
    }

    private IEnumerator CameraZoomOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < cameraZoomDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalCameraPosition, elapsedTime / cameraZoomDuration);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, originalCameraRotation, elapsedTime / cameraZoomDuration);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, originalCameraFieldOfView, elapsedTime / cameraZoomDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and FOV are set correctly
        mainCamera.transform.position = originalCameraPosition;
        mainCamera.transform.rotation = originalCameraRotation;
        mainCamera.fieldOfView = originalCameraFieldOfView;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the interaction range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);

        // Draw a line to visualize the camera focus point
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * focusPointDistance);
    }
}
