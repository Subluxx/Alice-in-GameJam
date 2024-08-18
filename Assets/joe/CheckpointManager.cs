using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private Checkpoint currentCheckpoint;
    private CharacterMovement player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CheckpointManager instance set.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
        if (player == null)
        {
            Debug.LogError("CharacterMovement script not found in the scene.");
        }
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.SetInactive();
            Debug.Log("Previous checkpoint deactivated.");
        }

        currentCheckpoint = checkpoint;
        currentCheckpoint.SetActive();
        Debug.Log("New checkpoint set at position: " + checkpoint.transform.position);
    }

    public void RestartFromCheckpoint()
    {
        if (currentCheckpoint != null && player != null)
        {
            player.transform.position = currentCheckpoint.transform.position;
            // Optionally reset player status (e.g., health, size, etc.)
        }
        else
        {
            Debug.LogWarning("No checkpoint set or player not found.");
        }
    }
}
