using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Public fields to be set in the Unity Inspector
    public Material highlightMaterial; // Material used for highlighting the item
    private Material originalMaterial; // Original material of the item
    public GameObject sparkleEffectPrefab; // Prefab for the sparkle effect
    public AudioClip clickSound; // Audio clip for click sound
    public GameObject tooltip; // UI element for the tooltip
    public float respawnTime = 5f; // Time in seconds before the item respawns
    public float interactionRange = 5f; // Range within which the item can be interacted with

    private Renderer itemRenderer;
    private AudioSource audioSource;
    private CharacterMovement characterController; // Reference to the player character's controller
    private bool isPlayerInRange = false;
    private Animator animator;

    private void Start()
    {
        // Get the Renderer component of the item
        itemRenderer = GetComponent<Renderer>();
        // Store the original material
        if (itemRenderer != null)
        {
            originalMaterial = itemRenderer.material;
        }

        // Find the CharacterController3D component (assuming it's on the player)
        characterController = FindObjectOfType<CharacterMovement>();

        // Add an AudioSource component if it doesn't already exist
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Ensure it doesn't play automatically

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is within interaction range
        if (characterController != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, characterController.transform.position);
            isPlayerInRange = distanceToPlayer <= interactionRange;
        }
    }

    // Method called when the pointer (mouse) enters the item area
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPlayerInRange)
        {
            if (itemRenderer != null && highlightMaterial != null)
            {
                itemRenderer.material = highlightMaterial; // Apply highlight material
            }

            if (tooltip != null)
            {
                tooltip.SetActive(true); // Show tooltip
            }
        }
    }

    // Method called when the pointer (mouse) exits the item area
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPlayerInRange)
        {
            if (itemRenderer != null && originalMaterial != null)
            {
                itemRenderer.material = originalMaterial; // Revert to original material
            }

            foreach (Transform child in transform)
            {
                if (child.CompareTag("SparkleEffect"))
                {
                    Destroy(child.gameObject); // Remove sparkle effects
                }
            }

            if (tooltip != null)
            {
                tooltip.SetActive(false); // Hide tooltip
            }
        }
    }

    // Method called when the item is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlayerInRange && characterController != null)
        {
            // Play click sound
            if (clickSound != null)
            {
                audioSource.PlayOneShot(clickSound);
            }

            // Instantiate the sparkle effect
            if (sparkleEffectPrefab != null)
            {
                Instantiate(sparkleEffectPrefab, transform.position, Quaternion.identity);
            }

            // Perform the size change based on item tag
            if (CompareTag("GrowItem"))
            {
                characterController.ChangeSize(characterController.sizeChangeFactor);
            }
            else if (CompareTag("ShrinkItem"))
            {
                characterController.ChangeSize(1 / characterController.sizeChangeFactor);
            }

            // Deactivate the item GameObject and schedule respawn
            gameObject.SetActive(false);
            Invoke("StartRespawnSequence", respawnTime);
        }
    }

    // Method to start the respawn sequence
    private void StartRespawnSequence()
    {
        gameObject.SetActive(true);
        PlayRespawnAnimation();
    }

    // Method to play the respawn animation
    private void PlayRespawnAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("PlayRespawn"); // Trigger the animation to scale the item
        }
    }
}
