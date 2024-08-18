using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Transform leftCameraPosition;  // Set in the inspector: Camera position for the left room
    public Transform rightCameraPosition; // Set in the inspector: Camera position for the right room

    private Camera mainCamera;
    private bool transitioning = false;
    private Transform targetPosition;
    private float transitionDuration = 1f; // Duration of the transition
    private float transitionTime = 0f;
    private bool isInLeftRoom = true; // Start in the left room

    private void Start()
    {
        mainCamera = Camera.main; // Get the main camera

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Ensure a Camera is tagged as 'MainCamera'.");
        }

        if (leftCameraPosition == null || rightCameraPosition == null)
        {
            Debug.LogError("Camera positions are not assigned. Assign them in the inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Toggle camera position based on the current room
            if (isInLeftRoom)
            {
                // Transition to the right room
                targetPosition = rightCameraPosition;
                Debug.Log("Transitioning to the right room.");
            }
            else
            {
                // Transition to the left room
                targetPosition = leftCameraPosition;
                Debug.Log("Transitioning to the left room.");
            }

            if (targetPosition != null)
            {
                transitioning = true;
                transitionTime = 0f; // Reset transition time
                isInLeftRoom = !isInLeftRoom; // Toggle room status
            }
            else
            {
                Debug.LogWarning("Target position is null.");
            }
        }
    }

    private void Update()
    {
        if (transitioning)
        {
            // Smoothly move the camera to the target position
            transitionTime += Time.deltaTime / transitionDuration;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition.position, transitionTime);

            // Stop transitioning when the camera is close to the target position
            if (transitionTime >= 1f)
            {
                transitioning = false;
                Debug.Log("Transition complete.");
            }
        }
    }
}
