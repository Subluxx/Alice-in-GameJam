using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Checkpoint currentCheckpoint;
    private CharacterMovement player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.SetInactive();
        }

        currentCheckpoint = checkpoint;
        currentCheckpoint.SetActive();
    }

    public void RestartFromCheckpoint()
    {
        if (currentCheckpoint != null && player != null)
        {
            player.transform.position = currentCheckpoint.transform.position;
            // Optionally reset player status (e.g., health, size, etc.)
        }
    }
}