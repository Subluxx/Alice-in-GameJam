using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Material highlightMaterial;
    private Material originalMaterial;
    public GameObject sparkleEffectPrefab;
    public AudioClip clickSound;
    public GameObject tooltip;
    public float respawnTime = 5f;
    public float interactionRange = 5f;

    private Renderer itemRenderer;
    private AudioSource audioSource;
    private CharacterMovement characterController;
    private bool isPlayerInRange = false;
    private Animator animator;
    private GameObject currentSparkleEffect;

    private void Start()
    {
        itemRenderer = GetComponent<Renderer>();
        if (itemRenderer != null)
        {
            originalMaterial = itemRenderer.material;
        }

        characterController = FindObjectOfType<CharacterMovement>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (characterController != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, characterController.transform.position);
            isPlayerInRange = distanceToPlayer <= interactionRange;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPlayerInRange)
        {
            if (itemRenderer != null && highlightMaterial != null)
            {
                itemRenderer.material = highlightMaterial;
            }

            if (tooltip != null)
            {
                tooltip.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPlayerInRange)
        {
            if (itemRenderer != null && originalMaterial != null)
            {
                itemRenderer.material = originalMaterial;
            }

            if (currentSparkleEffect != null)
            {
                Destroy(currentSparkleEffect);
                currentSparkleEffect = null;
            }

            if (tooltip != null)
            {
                tooltip.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlayerInRange && characterController != null)
        {
            if (clickSound != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
            else
            {
                Debug.LogWarning("Click sound not assigned.");
            }

            if (sparkleEffectPrefab != null)
            {
                currentSparkleEffect = Instantiate(sparkleEffectPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Sparkle effect prefab not assigned.");
            }

            if (CompareTag("GrowItem"))
            {
                characterController.ChangeSize(characterController.sizeChangeFactor);
            }
            else if (CompareTag("ShrinkItem"))
            {
                characterController.ChangeSize(1 / characterController.sizeChangeFactor);
            }

            gameObject.SetActive(false);
            Invoke("StartRespawnSequence", respawnTime);
        }
    }

    private void StartRespawnSequence()
    {
        gameObject.SetActive(true);
        PlayRespawnAnimation();
    }

    private void PlayRespawnAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("PlayRespawn");
        }
    }
}
