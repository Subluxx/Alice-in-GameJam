using System.Collections;
using UnityEngine;

public class TeleportTransition : MonoBehaviour
{
    public Transform cameraPosition;          // The point where the camera will transition to
    public Transform teleportPoint;           // The point where the player will be teleported to
    public float transitionDuration = 1f;     // Duration of the camera transition
    public float shrinkDuration = 0.5f;       // Duration for the player to shrink
    public float respawnDuration = 0.5f;      // Duration for the player to reappear
    public Vector3 smallPlayerScale = Vector3.one * 0.1f; // The scale to temporarily shrink the player

    private Camera mainCamera;
    private GameObject player;
    private Rigidbody playerRigidbody;         // Reference to the player's Rigidbody
    private bool transitioning = false;
    private bool isTeleporting = false;        // Flag to ensure only one teleportation at a time
    private float transitionTime = 0f;

    private void Start()
    {
        mainCamera = Camera.main; // Get the main camera

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Ensure a Camera is tagged as 'MainCamera'.");
        }

        if (cameraPosition == null || teleportPoint == null)
        {
            Debug.LogError("Camera position or teleport point not assigned. Assign them in the inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            player = other.gameObject;
            playerRigidbody = player.GetComponent<Rigidbody>();

            if (player != null && playerRigidbody != null)
            {
                // Start the teleport sequence
                StartCoroutine(TeleportSequence());
            }
        }
    }

    private IEnumerator TeleportSequence()
    {
        if (isTeleporting) yield break; // Ensure only one teleportation at a time
        isTeleporting = true; // Set the flag to true to prevent re-entry

        // Lock player movement constraints
        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        // Shrink the player
        yield return StartCoroutine(SmoothScale(smallPlayerScale, shrinkDuration));
        player.SetActive(false); // hide the player after we are small

        // Smoothly move the camera to the new position
        yield return StartCoroutine(SmoothCameraTransition(cameraPosition.position, cameraPosition.rotation));

        // Move player to the new position and reset size
        player.transform.position = teleportPoint.position;
        player.transform.localScale = Vector3.zero; // Ensure player is initially invisible
        
        // Optional: Smoothly reposition the player to avoid clipping issues
        yield return new WaitForSeconds(0.1f); // Short delay to ensure visibility change
        player.SetActive(true); // Show the player again

        // Resize player back to normal size
        yield return StartCoroutine(SmoothScale(Vector3.one, respawnDuration));

        // Unlock player movement constraints
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ; // Keep rotation frozen if necessary and movement along the z axis

        isTeleporting = false; // Reset the flag to allow future teleportations
    }

    private IEnumerator SmoothScale(Vector3 targetScale, float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialScale = player.transform.localScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            player.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            yield return null;
        }

        player.transform.localScale = targetScale; // Ensure final scale is set
    }

    private IEnumerator SmoothCameraTransition(Vector3 targetPosition, Quaternion targetRotation)
    {
        transitioning = true;
        transitionTime = 0f;
        Vector3 initialPosition = mainCamera.transform.position;
        Quaternion initialRotation = mainCamera.transform.rotation;

        while (transitionTime < 1f)
        {
            transitionTime += Time.deltaTime / transitionDuration;
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, transitionTime);
            mainCamera.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, transitionTime);
            yield return null;
        }

        transitioning = false;
    }
}
