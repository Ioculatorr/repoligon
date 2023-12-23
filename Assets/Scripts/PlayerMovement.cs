using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float jumpCooldown = 1.0f;

    private float lastJumpTime;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;
    [SerializeField] private Headbobbing headbobbing;
    [SerializeField] private UnityEvent movementEvent;


    [SerializeField] private float minFallDamageVelocity = 9f;
    private float fallStartY;

    bool canDie = true;


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

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        // Check if enough time has passed since the last jump
        if (Time.time - lastJumpTime >= jumpCooldown)
        {
            Jump();
        }

        // Call the headbobbing method with the information about movement
        headbobbing.SetIsMoving(move.magnitude > 0.1f);







        // If the player starts falling, record the starting Y position
        if (!isGrounded && Mathf.Approximately(fallStartY, 0f))
        {
            fallStartY = transform.position.y;
        }

        // If the player has landed, calculate fall damage
        if (isGrounded && !Mathf.Approximately(fallStartY, 0f))
        {
            float fallDistance = fallStartY - transform.position.y;

            if (fallDistance > 0 && fallDistance >= minFallDamageVelocity && canDie == true)
            {
                movementEvent.Invoke();
                canDie = false;
            }
        }
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
