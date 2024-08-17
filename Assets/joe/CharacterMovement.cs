using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{ 
       // Movement and control variables
    public float walkSpeed = 10f;
    public float sprintSpeed = 15f;
    public float airControlMultiplier = 0.5f;
    public float acceleration = 15f;
    public float deceleration = 15f;
    public float smoothTime = 0.1f;
    public float friction = 5f;
    public float gravity = 12f;
    public float apexGravityMultiplier = 0.6f;
    public float fastFallForce = 10f;
    public float groundCorrectionForce = 5f;

    // Jump variables
    public float jumpForce = 9f;
    public float jumpBufferTime = 0.1f;
    public float coyoteTime = 0.1f;
    public float glideFactor = 0.6f;
    public float glideSpeed = 0.1f;

    // Size change variables
    public float sizeChangeFactor = 1.1f;  // Factor to change size
    public float sizeChangeDuration = 0.5f;  // Duration of size change effect
    public Vector3 maxSize = new Vector3(2.2f, 2.2f, 2.2f);  // Maximum allowed size
    public Vector3 minSize = new Vector3(0.8f, 0.8f, 0.8f);  // Minimum allowed size

    private float currentSpeed = 0f;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 movement;
    private Vector3 targetDirection;
    private Vector3 targetScale;
    private bool isScaling = false;
    private float scaleProgress = 0f;
    private bool isGrounded;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private bool isApexReached = false;
    private bool isJumping = false;
    private float jumpTimeCounter;
    private float jumpBufferCounter = 0f;
    private float coyoteTimeCounter = 0f;
    private bool jumpInput = false;
    private bool fastFallInput = false;
    private bool holdJumpInput = false;
    private Collider groundCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        targetScale = transform.localScale;
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Ensure a Box Collider is attached for ground detection
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true; // Set as trigger for collision detection
    }

    void Update()
    {
        // Handle player inputs in Update
        jumpInput = Input.GetKeyDown(KeyCode.W);
        holdJumpInput = Input.GetKey(KeyCode.W);
        fastFallInput = Input.GetKey(KeyCode.S);

        float moveX = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;  // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;   // Move right
        }

        targetDirection = new Vector3(moveX, 0f, 0f).normalized;

        if (jumpInput)
        {
            jumpBufferCounter = jumpBufferTime;  // Reset jump buffer when jump is pressed
        }
    }

    void FixedUpdate()
    {
        // Handle coyote time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;  // Reset coyote time when grounded
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;  // Decrease coyote time while airborne
        }

        // Handle jump buffering
        jumpBufferCounter -= Time.fixedDeltaTime;  // Decrease jump buffer over time

        // Apply air control multiplier if in the air
        float currentAcceleration = isGrounded ? acceleration : acceleration * airControlMultiplier;
        float currentDeceleration = isGrounded ? deceleration : deceleration * airControlMultiplier;

        // Determine target speed
        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        if (targetDirection.magnitude > 0)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity.x, smoothTime);
        }
        else
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref currentVelocity.x, smoothTime);
        }

        // Smoothly apply movement using Rigidbody
        movement = targetDirection * currentSpeed;

        if (isGrounded)
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, 0f);
        }
        else
        {
            // Apply glide effect when changing direction in the air
            Vector3 desiredVelocity = new Vector3(movement.x, rb.velocity.y, 0f);
            rb.velocity = Vector3.Lerp(rb.velocity, desiredVelocity, glideSpeed);
        }

        // Variable Jump Height with Jump Buffer and Coyote Time
        if (jumpBufferCounter > 0 && (coyoteTimeCounter > 0 || isGrounded))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // Apply jump force
            isJumping = true;
            isApexReached = false;

            // Reset jump buffer to prevent double jumps
            jumpBufferCounter = 0f;
        }

        if (holdJumpInput && isJumping)
        {
            if (rb.velocity.y > 0)  // While ascending
            {
                rb.velocity += Vector3.up * (jumpForce / 2) * Time.fixedDeltaTime; // Increase jump force while holding
            }
        }

        if (!holdJumpInput && rb.velocity.y > 0)
        {
            isJumping = false;
        }

        // Fast falling
        if (fastFallInput && !isGrounded)
        {
            rb.AddForce(Vector3.down * fastFallForce, ForceMode.VelocityChange);
        }

        // Apply custom gravity logic
        if (!isGrounded)
        {
            if (rb.velocity.y > 0 && !isApexReached)
            {
                rb.AddForce(Vector3.down * gravity * apexGravityMultiplier, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
                if (rb.velocity.y <= 0)
                {
                    isApexReached = true;
                }
            }
        }

        // Correct position to avoid clipping
        if (isGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Max(rb.velocity.y, -groundCorrectionForce), rb.velocity.z);
        }

        // Handle friction on landing/takeoff
        if (isGrounded && Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            rb.velocity = new Vector3(rb.velocity.x * (1 - friction * Time.deltaTime), rb.velocity.y, rb.velocity.z);
        }

        // Handle smooth scaling if scaling is in progress
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

    // Method to change the character's size
    public void ChangeSize(float factor)
    {
        // Calculate the new size
        Vector3 newSize = transform.localScale * factor;

        // Ensure the new size is within limits
        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
        newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);
        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

        targetScale = newSize;
        scaleProgress = 0f;
        isScaling = true;
    }

    void OnCollisionStay(Collision collision)
    {
        // Check if the collision is with ground
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the collision is with ground
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}