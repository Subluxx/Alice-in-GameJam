using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterMovement : MonoBehaviour
{
    // Movement parameters
    public float walkSpeed = 10f;
    public float sprintSpeed = 15f;
    public float airControlMultiplier = 0.1f;
    public float acceleration = 15f;
    public float deceleration = 15f;
    public float smoothTime = 0.2f;
    public float gravity = 12f;
    public float apexGravityMultiplier = 0.4f;
    public float fastFallForce = 1f;
    public float groundCorrectionForce = 4f;

    // Jump parameters
    public float jumpForce = 5f;
    public float jumpBufferTime = 0.2f;
    public float coyoteTime = 0.1f;
    public float glideFactor = 0.8f;
    public float glideSpeed = 0.1f;

    // Size change parameters
    [SerializeField] public float currentSize = 0f;
    public float sizeChangeFactor = 2f;
    public float sizeChangeDuration = 2f;
    public Vector3 maxSize = new Vector3(3f, 3f, 3f);
    public Vector3 minSize = new Vector3(0.3f, 0.3f, 0.3f);

    // Wall collision parameters
    public float wallPushbackForce = 2f; // Force to push player away from the wall
    public float wallDetectionRadius = 1f; // Radius for detecting walls
    public float bounceLockTime = 0.1f; // Time to lock movement after bouncing off a wall

    private float currentSpeed = 0f;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 movement;
    private Vector3 targetDirection;
    private Vector3 targetScale;
    private bool isScaling = false;
    private float scaleProgress = 0f;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isBouncing = false;
    private float jumpBufferCounter = 0f;
    private float coyoteTimeCounter = 0f;
    private bool jumpInput = false;
    private bool fastFallInput = false;
    private bool holdJumpInput = false;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        targetScale = transform.localScale;
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance.RestartFromCheckpoint();
            }
        }

        if (isBouncing) return; // Lock movement if bouncing

        jumpInput = Input.GetKeyDown(KeyCode.W);
        holdJumpInput = Input.GetKey(KeyCode.W);
        fastFallInput = Input.GetKey(KeyCode.S);

        float moveX = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }

        targetDirection = new Vector3(moveX, 0f, 0f).normalized;

        if (jumpInput)
        {
            jumpBufferCounter = jumpBufferTime;
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJumping();
        HandleGravity();
        HandleScaling();
        HandleWallCollisions(); // Handle wall collisions
    }

    private void HandleMovement()
    {
        if (isBouncing) return; // Skip movement handling if bouncing

        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        float currentAcceleration = isGrounded ? acceleration : acceleration * airControlMultiplier;
        float currentDeceleration = isGrounded ? deceleration : deceleration * airControlMultiplier;

        if (targetDirection.magnitude > 0)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity.x, smoothTime);
        }
        else
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref currentVelocity.x, smoothTime);
        }

        movement = targetDirection * currentSpeed;

        if (isGrounded)
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, 0f);
        }
        else
        {
            Vector3 desiredVelocity = new Vector3(movement.x, rb.velocity.y, 0f);
            rb.velocity = Vector3.Lerp(rb.velocity, desiredVelocity, glideSpeed);
        }
    }

    private void HandleJumping()
    {
        if (jumpBufferCounter > 0 && (coyoteTimeCounter > 0 || isGrounded))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isJumping = true;
            jumpBufferCounter = 0f;
        }

        if (holdJumpInput && isJumping)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity += Vector3.up * (jumpForce / 2) * Time.fixedDeltaTime;
            }
        }

        if (!holdJumpInput && rb.velocity.y > 0)
        {
            isJumping = false;
        }

        if (fastFallInput && !isGrounded)
        {
            rb.AddForce(Vector3.down * fastFallForce, ForceMode.VelocityChange);
        }
    }

    private void HandleGravity()
    {
        if (!isGrounded)
        {
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * gravity * apexGravityMultiplier, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
            }
        }

        if (isGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Max(rb.velocity.y, -groundCorrectionForce), rb.velocity.z);
        }
    }

    private void HandleScaling()
    {
        if (isScaling)
        {
            scaleProgress += Time.deltaTime / sizeChangeDuration;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleProgress);

            if (scaleProgress >= 1f)
            {
                isScaling = false;
            }
        }
    }

    private void HandleWallCollisions()
    {
        if (isBouncing) return; // Skip wall collision handling if bouncing

        // Check for collisions with walls
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, wallDetectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Wall"))
            {
                Vector3 pushbackDirection = (transform.position - collider.transform.position).normalized;
                rb.AddForce(pushbackDirection * wallPushbackForce, ForceMode.VelocityChange);

                // Trigger bounce logic
                if (!isBouncing)
                {
                    isBouncing = true;
                    StartCoroutine(UnlockMovementAfterTime(bounceLockTime));
                }
            }
        }
    }

    private IEnumerator UnlockMovementAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isBouncing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            // Handle wall collision bounce
            if (!isBouncing)
            {
                Vector3 pushbackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(pushbackDirection * wallPushbackForce, ForceMode.VelocityChange);

                isBouncing = true;
                StartCoroutine(UnlockMovementAfterTime(bounceLockTime));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void ChangeSize(float factor)
    {
        Vector3 newSize = transform.localScale * factor;
        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
        newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);
        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

        currentSize = newSize.x; // for viewing
        // change the bounce lock time depending on the scale of the player
        switch (newSize.x)
        {
            case 3f:
                bounceLockTime = 0.5f;
                wallPushbackForce = 6f;
                break;
            case 2f:
                bounceLockTime = 0.3f;
                wallPushbackForce = 5.5f;
                break;
            case <= 1f:
                bounceLockTime = 0.1f;
                wallPushbackForce = 5f;
                break;
        }
        targetScale = newSize;
        scaleProgress = 0f;
        isScaling = true;
    }
}
