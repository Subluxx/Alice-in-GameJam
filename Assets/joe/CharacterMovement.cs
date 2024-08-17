using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
        public float walkSpeed = 10f;       // Increased speed for faster movement
    public float sprintSpeed = 15f;     // Increased sprint speed
    public float acceleration = 10f;    // Faster acceleration
    public float deceleration = 10f;    // Faster deceleration
    public float smoothTime = 0.05f;    // Smoother movement
    public float sizeChangeFactor = 1.1f;
    public float sizeChangeDuration = 0.5f;
    public float jumpForce = 10f;       // Adjusted jump force
    public float fastFallForce = 20f;   // Faster fall force
    public float gravity = 9.81f;        // Ensure gravity is positive for downward force
    public float groundCheckDistance = 0.2f;  // Distance for the ground check
    public string groundTag = "Ground";

    private float currentSpeed = 0f;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 movement;
    private Vector3 targetDirection;
    private Vector3 targetScale;
    private bool isScaling = false;
    private float scaleProgress = 0f;
    private bool isGrounded;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Prevent the Rigidbody from rotating
        targetScale = transform.localScale;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        // Check if the player is grounded using a Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, LayerMask.GetMask(groundTag));

        if (isGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, -2f, rb.velocity.z); // Reset the downward velocity when grounded
        }

        // Handle movement based on input
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

        float targetSpeed = walkSpeed; // Adjust for walk speed; add sprint logic if needed

        if (targetDirection.magnitude > 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        Vector3 targetMovement = targetDirection * currentSpeed;
        movement = Vector3.SmoothDamp(movement, targetMovement, ref currentVelocity, smoothTime);

        // Apply movement along the X axis using Rigidbody
        rb.velocity = new Vector3(movement.x, rb.velocity.y, 0f);

        // Jumping and fast falling
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // Apply jump force
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.down * fastFallForce, ForceMode.VelocityChange);
        }

        // Apply gravity manually if not using Rigidbody's gravity
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            ChangeSize(sizeChangeFactor);
            Destroy(other.gameObject);
        }
    }

    void ChangeSize(float factor)
    {
        targetScale = transform.localScale * factor;
        scaleProgress = 0f;
        isScaling = true;
    }
}
