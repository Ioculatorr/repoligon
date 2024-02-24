using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    [Header("Movement")]
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float waterDrag = 2f;
    [SerializeField] private float drownForce = 10f;
    
    
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float jumpCooldown = 1f;
    [SerializeField] private float airMultiplier = 0.5f;
    
    [Header("Ground Check")] 
    
    [SerializeField] private float playerHeight = 2f;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask Water;
    
    private bool readyToJump = true;
    public bool grounded;
    public bool swimming;


    [Header("Keybinds")] 
    
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode drownKey = KeyCode.LeftControl;

    
    

    [SerializeField] private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        
        
        SpeedControl();
        
        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        if (swimming)
        {
            rb.drag = waterDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        
        //Debug.Log(rb.velocity.magnitude);
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && (grounded || swimming))
        {
            readyToJump = false;
            
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKey(drownKey) && swimming)
        {
            rb.AddForce(Vector3.down * moveSpeed * drownForce, ForceMode.Acceleration);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Acceleration);
        }
        if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Acceleration);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed && !swimming)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    private void Jump()
    {
        // reset y velocity to jump exactly the same height every time
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void InWater()
    {
        swimming = true;
        //rb.useGravity = false;
    }

    public void OutWater()
    {
        swimming = false;
        //rb.useGravity = true;
    }
}
