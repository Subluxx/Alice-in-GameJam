using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionTrigger : MonoBehaviour
{
    public int roomIndex;  // The index of the room this door leads to
    private SceneZoomController sceneZoomController;

    private void Start()
    {
        // Find the SceneZoomController script attached to the player or another object
        sceneZoomController = FindObjectOfType<SceneZoomController>();

        if (sceneZoomController == null)
        {
            Debug.LogError("SceneZoomController not found in the scene. Ensure it is attached to an object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sceneZoomController != null)
        {
            // Update the current room index when the player enters the room
            sceneZoomController.SetCurrentRoomIndex(roomIndex);
        }
    }
}
