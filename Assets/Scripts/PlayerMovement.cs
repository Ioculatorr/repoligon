using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject playerCam;

    [Header("Movement")]

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Jumping")]

    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float jumpCooldown = 1.0f;

    [Header("Dashing")]

    [SerializeField] private float dashSpeed = 24f;
    [SerializeField] private float dashTime = 0.5f; // Dash duration
    [SerializeField] private float dashCooldown = 3.0f;  // Dash cooldown
    
    [Header("Audio")]
    
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource footstepAudio;
    [SerializeField] private AudioClip[] footstepClips;

    private float lastJumpTime;
    private float lastDashTime;

    private float currentSpeed;
    private float targetSpeed;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;
    [SerializeField] private Headbobbing headbobbing;
    [SerializeField] private UnityEvent fallDeathEvent;


    [SerializeField] private float minFallDistance = 9f;
    private float fallStartY = 0f;

    bool canDie = true;
    //bool isDashing = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FootstepsSound());
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Accelerate towards the target speed
        //currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
        


        // Check if enough time has passed since the last jump
        if (Time.time - lastJumpTime >= jumpCooldown)
        {
            Jump();
        }

        if (Time.time - lastDashTime >= dashCooldown)
        {
            Dash();
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



            //Debug.Log(fallDistance.ToString());

            if (fallDistance >= 0 && fallDistance >= minFallDistance && canDie == true)
            {
                fallDeathEvent.Invoke();
                this.enabled = false;
                canDie = false;
            }
            else if (fallDistance >= 0.1f)
            {
                playerCam.transform.DOShakeRotation(1f, fallDistance * 2f, 6, 15f, true)
                            .OnComplete(() =>
                            {
                                playerCam.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
                            });
            }

            fallStartY = 0f;
        }
    }

    void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //this.GetComponent<AudioSource>().Play();
            jumpAudio.Play();
            
            // Update the last jump time
            lastJumpTime = Time.time;
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(PerformDash());

            lastDashTime = Time.time;
        }
    }

    IEnumerator PerformDash()
    {
        //isDashing = true;

        float dashTimer = dashTime;
        while (dashTimer > 0f)
        {
            // Move the player in the dash direction
            characterController.Move(transform.forward * dashSpeed * Time.deltaTime);

            // Update timer
            dashTimer -= Time.deltaTime;

            yield return null;
        }

        //isDashing = false;
    }

    IEnumerator FootstepsSound()
    {
        while (true)
        {
            // Check if the player is moving and grounded to play footsteps sound
            if (isGrounded && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                // Play the footstep sound
                AudioClip clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
                footstepAudio.PlayOneShot(clip);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
