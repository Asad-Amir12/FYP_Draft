using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private LayerMask groundLayer; // Layer for ground detection
    [SerializeField] private Transform groundCheck; // Transform for ground check position
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius for ground check
    [SerializeField] private GameObject sword; // Reference to the sword object

    private Vector3 moveDirection;
    private Rigidbody rb;
    private Vector2 movement;
    public InputActionProperty moveAction;
    public InputActionProperty jumpAction;
    private Animator animator;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        moveAction.action.Enable();
        jumpAction.action.Enable();

        // Find the sword object by tag and hide it
        sword = GameObject.FindGameObjectWithTag("Sword");
        if (sword != null)
        {
            sword.SetActive(false); // Hide the sword initially
        }
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckGrounded();
        CheckJump();

        // Check for left mouse click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleSword();
        }
    }

    private void HandleSword()
    {
        if (sword != null)
        {
            sword.SetActive(true); // Make the sword visible
            animator.Play("RightSword"); // Play the "rightSword" animation
            StartCoroutine(HideSwordAfterDelay(2f)); // Hide the sword after 2 seconds
        }
    }

    private IEnumerator HideSwordAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (sword != null)
        {
            sword.SetActive(false); // Hide the sword again
        }
        animator.SetBool("IsUsingSword", false);
    }

    private void Move()
    {
        movement = moveAction.action.ReadValue<Vector2>();

        // Get the player's forward and right directions (influenced by the camera's rotation)
        Vector3 playerForward = transform.forward;
        Vector3 playerRight = transform.right;

        // Calculate the move direction relative to the player's local forward and right directions
        moveDirection = (playerForward * movement.y + playerRight * movement.x).normalized;

        float verticalVelocity = rb.velocity.y;

        // Apply gravity only if the player is not grounded
        if (!isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        else if (verticalVelocity < 0)
        {
            verticalVelocity = 0f; // Reset vertical velocity when grounded
        }

        // Rotate the player only when moving forward or backward
        if (movement.y > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Check if the player is moving and pressed shift then call run else if not moving call idle
        if (movement.magnitude > 0.1f && Keyboard.current.leftShiftKey.isPressed)
        {
            Run();
        }
        else if (movement.magnitude > 0.1f)
        {
            Walk();
        }
        else
        {
            Idle();
        }

        rb.velocity = new Vector3(moveDirection.x * moveSpeed, verticalVelocity, moveDirection.z * moveSpeed);
    }

    private void CheckJump()
    {
        if (jumpAction.action.triggered && isGrounded)
        {
            animator.SetTrigger("Jump");

            Vector3 velocity = rb.velocity;
            velocity.y = 0f; // Clear any downward force
            rb.velocity = velocity;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Better jump
            isGrounded = false;
        }
    }

    private void Idle()
    {
        moveDirection = Vector3.zero;
        animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void CheckGrounded()
    {
        // Check if a raycast hits the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.75f, groundLayer);
        
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }
}