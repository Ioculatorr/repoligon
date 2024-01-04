using UnityEngine;

public class PlayerRigidbodyMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 2f;
    public float jumpForce = 15f;
    public float jumpCooldown = 1.0f;

    public float groundRaycastDistance = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;
    private float lastJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Lock cursor for a better first-person experience
        Cursor.lockState = CursorLockMode.Locked;
        lastJumpTime = -jumpCooldown;
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseLook();
        HandleJump();
        HandleDash();
        CheckGround();
    }

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * movementSpeed * Time.deltaTime;
        movement = transform.TransformDirection(movement); // Convert movement to be relative to the player's rotation

        rb.MovePosition(rb.position + movement);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player (body) around the Y-axis
        transform.Rotate(Vector3.up * mouseX * rotationSpeed);

        // Rotate the camera (head) around the X-axis with clamping to prevent over-rotation
        float currentRotation = Camera.main.transform.eulerAngles.x;
        float newRotation = Mathf.Clamp(currentRotation - mouseY * rotationSpeed, 0f, 90f);

        Camera.main.transform.rotation = Quaternion.Euler(newRotation, transform.eulerAngles.y, 0f);
    }

    void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && Time.time - lastJumpTime > jumpCooldown)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJumpTime = Time.time;
        }
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 dashDirection = Camera.main.transform.forward;

            rb.AddForce(dashDirection * jumpForce, ForceMode.Impulse);
        }
    }
    void CheckGround()
    {
        // Perform a raycast below the player to check if it's grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundRaycastDistance))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                // The player is grounded
                Debug.DrawRay(transform.position, Vector3.down * groundRaycastDistance, Color.green);
                isGrounded = true;
            }
        }
        else
        {
            // The player is not grounded
            Debug.DrawRay(transform.position, Vector3.down * groundRaycastDistance, Color.red);
            isGrounded = false;
        }
    }
}
