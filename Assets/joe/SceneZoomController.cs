using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneZoomController : MonoBehaviour
{
    public Transform[] cameraPositions;           // Array of camera positions for each room
    public Transform mainViewPosition; // Position for the main view (adjust as needed)
    public float initialViewDuration = 2f;        // Time to stay in the initial view before zooming in
    public float transitionDuration = 1.5f;       // Duration for camera transitions

    private Camera mainCamera;
    private bool isZoomedIn = false;              // To track if we're zoomed into a room
    private bool isTransitioning = false;         // To track if the camera is currently transitioning
    private int currentRoomIndex = 0;             // Index of the current room's camera position
    private CharacterMovement playerMovement;        // Reference to the PlayerMovement script

    void Start()
    {
        mainCamera = Camera.main;

        if (cameraPositions == null || cameraPositions.Length == 0)
        {
            Debug.LogError("Camera positions not assigned or empty. Assign them in the inspector.");
            return;
        }

        // Get the PlayerMovement component attached to the player
        playerMovement = FindObjectOfType<CharacterMovement>();

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found. Ensure it is attached to the player.");
            return;
        }

        // Start the sequence: show the whole scene first, then zoom in to the initial room
        /*StartCoroutine(InitialViewAndZoomIn());*/
    }

    void Update()
    {
        // Handle zooming out when Q is pressed
        if (Input.GetKeyDown(KeyCode.Q) && !isTransitioning)
        {
            if (isZoomedIn)
            {
                StartCoroutine(ZoomOutToMainView());
            }
            else
            {
                /*StartCoroutine(ZoomInToCurrentRoom());*/
            }
        }
    }

    private IEnumerator InitialViewAndZoomIn()
    {
        // Show the main view of the scene
        mainCamera.transform.position = mainViewPosition.transform.position;

        // Lock player movement while in the main view
        LockPlayerMovement(true);

        // Wait for a couple of seconds to view the entire scene
        yield return new WaitForSeconds(initialViewDuration);

        // Then zoom into the initial room (camera position 1)
        yield return StartCoroutine(ZoomInToRoom(0));
    }

    public void SetCurrentRoomIndex(int index)
    {
        if (index >= 0 && index < cameraPositions.Length)
        {
            currentRoomIndex = index;
        }
        else
        {
            Debug.LogError("Invalid room index: " + index);
        }
    }

    private IEnumerator ZoomInToRoom(int index)
    {
        isTransitioning = true;
        Vector3 initialPosition = mainCamera.transform.position;
        Vector3 targetPosition = cameraPositions[index].position;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / transitionDuration);
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        isZoomedIn = true;
        isTransitioning = false;

        // Unlock player movement when zoomed into the room
        LockPlayerMovement(false);
    }

    private IEnumerator ZoomInToCurrentRoom()
    {
        yield return StartCoroutine(ZoomInToRoom(currentRoomIndex));
    }

    private IEnumerator ZoomOutToMainView()
    {
        LockPlayerMovement(true);
        isTransitioning = true;
        Vector3 initialPosition = mainCamera.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(initialPosition, mainViewPosition.transform.position, elapsedTime / transitionDuration);
            yield return null;
        }

        mainCamera.transform.position = mainViewPosition.transform.position;
        isZoomedIn = false;
        isTransitioning = false;
    }

    // Method to lock or unlock player movement
    private void LockPlayerMovement(bool lockMovement)
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = !lockMovement;
        }
    }
}
