using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public Material inactiveMaterial;
    public Material activeMaterial;
    public string checkpointMessage = "Checkpoint Reached!";
    public float messageDisplayTime = 2f; // Time the message is displayed

    private Renderer checkpointRenderer;
    private bool isActive = false;
    private Text checkpointText; // Reference to the UI Text component

    private void Start()
    {
        checkpointRenderer = GetComponent<Renderer>();
        SetInactive();

        // Find the Text component in the scene
        checkpointText = GameObject.Find("CheckpointText").GetComponent<Text>();
        checkpointText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetActive();
            CheckpointManager.Instance.SetCheckpoint(this);
            DisplayCheckpointMessage();
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