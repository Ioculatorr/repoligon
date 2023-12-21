using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float jumpCooldown = 1.0f;

    private float lastJumpTime;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    [SerializeField] private Headbobbing headbobbing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0 )
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // Check if enough time has passed since the last jump
        if (Time.time - lastJumpTime >= jumpCooldown)
        {
            Jump();
        }

        // Call the headbobbing method with the information about movement
        headbobbing.SetIsMoving(move.magnitude > 0.1f);
    }

    void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            this.GetComponent<AudioSource>().Play();

            // Update the last jump time
            lastJumpTime = Time.time;
        }
    }
}
