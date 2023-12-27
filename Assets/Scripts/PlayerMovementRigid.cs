using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementRigid : MonoBehaviour
{
    [Header("Movement")]

    private float moveSpeed;
    [SerializeField] float walkSpeed;
    //[SerializeField] float sprintSpeed;

    [SerializeField] float groundDrag;


    [Header("Jumping")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    [SerializeField] float airDampingForce = 2f;
    bool readyToJump;

    [Header("Dashing")]
    [SerializeField] float dashForce;
    [SerializeField] float dashCooldown;
    bool readyToDash;

    //[Header("Crouching")]
    //[SerializeField] float crouchSpeed;
    //[SerializeField] float crouchYScale;

    private float startYScale;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    //[SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;
    //[SerializeField] KeyCode crouchKey = KeyCode.LeftControl;


    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    public bool isGrounded;

    [Header("Slope Handling")]
    [SerializeField] float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [SerializeField] Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    bool canDie;
    private float fallStartY = 0f;
    private float minFallDistance = 20f;

    [SerializeField] private Headbobbing headbobbing;

    public MovementState state;
    public enum MovementState
    {
        walking,
        //sprinting,
        //crouching,
        air
    }

    [SerializeField] private UnityEvent fallDamageEvent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        readyToDash= true;

        canDie = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {

        // ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        MovePlayer();
        SpeedControl();
        StateHandler();
        FallDamage();


        if (isGrounded)
        {
            rb.drag = groundDrag;
            // Call the headbobbing method with the information about movement
            headbobbing.SetIsMoving(rb.velocity.magnitude > 0.1f);
        }
        else
        {
            rb.drag = 0;
        }
    }

    //private void FixedUpdate()
    //{
    //    //MovePlayer();
    //}

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if(Input.GetKey(dashKey) && readyToDash)
        {
            readyToDash = false;

            Dash();

            Invoke(nameof(ResetDash), dashCooldown);
        }


        //// start crouching
        //if (Input.GetKeyDown(crouchKey))
        //{
        //    transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        //    rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        //}

        //if (Input.GetKeyUp(crouchKey))
        //{
        //    transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        //}
    }

    private void StateHandler()
    {
        
        //// Mode - Crouching
        //if (Input.GetKey(crouchKey))
        //{
        //    state = MovementState.crouching;
        //    moveSpeed = crouchSpeed;
        //}

        
        
        //// Mode - Sprint
        //if(isGrounded && Input.GetKey(sprintKey))
        //{
        //    state = MovementState.sprinting;
        //    moveSpeed = sprintSpeed;
        //}


        if (isGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        if (isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!isGrounded)
        {
            // Add damping force to slow down in the air
            rb.AddForce(-rb.velocity.normalized * airDampingForce, ForceMode.Acceleration);

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if(OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //limit velicity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }


    private void Jump()
    {
        exitingSlope = true;

        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 1f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }
    private void Dash()
    {
        exitingSlope = true;

        rb.AddForce(moveDirection.normalized * dashForce, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        readyToDash = true;

        exitingSlope = false;
    }

    private void FallDamage()
    {
        // If the player starts falling, record the starting Y position
        if (!isGrounded && Mathf.Approximately(fallStartY, 0f))
        {
            fallStartY = transform.position.y;
        }

        // If the player has landed, calculate fall damage
        if (isGrounded && !Mathf.Approximately(fallStartY, 0f))
        {
            float fallDistance = fallStartY - transform.position.y;

            Debug.Log(fallDistance.ToString());

            if (fallDistance > 0 && fallDistance >= minFallDistance && canDie == true)
            {
                fallDamageEvent.Invoke();
                this.enabled = false;
                canDie = false;
            }

            fallStartY = 0f;
        }
    }


    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;

    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
