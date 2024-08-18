using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public Material inactiveMaterial;
    public Material activeMaterial;
    public string checkpointMessage = "Checkpoint Reached! Press 'R' to return to this checkpoint";
    public float messageDisplayTime = 2f; // Time the message is displayed

    public Text checkpointText; // Reference to the UI Text component

    private Renderer checkpointRenderer;
    private bool isActive = false;
    private bool hasBeenVisited = false; // Flag to track if the checkpoint has been visited

    private void Start()
    {
        checkpointRenderer = GetComponent<Renderer>();
        SetInactive();

        if (checkpointText != null)
        {
            checkpointText.enabled = false;
        }
        else
        {
            Debug.LogError("Checkpoint Text is not assigned in the Inspector. Please assign the Text component.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasBeenVisited)
            {
                SetActive();
                CheckpointManager.Instance.SetCheckpoint(this);
                DisplayCheckpointMessage();
                hasBeenVisited = true; // Mark this checkpoint as visited
            }
        }
    }

    public void SetActive()
    {
        isActive = true;
        checkpointRenderer.material = activeMaterial;
    }

    public void SetInactive()
    {
        isActive = false;
        checkpointRenderer.material = inactiveMaterial;
    }

    private void DisplayCheckpointMessage()
    {
        if (checkpointText != null)
        {
            checkpointText.text = checkpointMessage;
            checkpointText.enabled = true;
            Invoke("HideCheckpointMessage", messageDisplayTime);
        }
    }

    private void HideCheckpointMessage()
    {
        if (checkpointText != null)
        {
            checkpointText.enabled = false;
        }
    }
}