using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rotationAmount = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 initialRotation;

    private PlayerMovementRigid boolCheck;

    void Start()
    {
        initialRotation = transform.localEulerAngles;
    }

    void Update()
    {
        // Get the player's movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the camera's rotation input
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

        // Calculate the rotation based on both player movement and camera movement
        Vector3 targetRotation = new Vector3(
            initialRotation.x - verticalInput * rotationAmount, // Reverse direction for player movement
            initialRotation.y + horizontalInput * rotationAmount,
            initialRotation.z
        );

        // Apply additional rotation based on camera movement speed
        targetRotation.y += mouseX;

        // Add rotation based on vertical velocity when in freefall
        if (!boolCheck.isGrounded && rb.velocity.y < 0)
        {
            float fallRotation = Mathf.Clamp(-rb.velocity.y * 2f, 0f, 30f); // Adjust the multiplier as needed
            targetRotation.x = -fallRotation; // Reverse direction for freefall
        }

        // Clamp rotation to avoid breaking the view
        targetRotation.x = Mathf.Clamp(targetRotation.x, initialRotation.x - rotationAmount, initialRotation.x + rotationAmount);
        targetRotation.y = Mathf.Clamp(targetRotation.y, initialRotation.y - rotationAmount, initialRotation.y + rotationAmount);

        // Use DOTween to smoothly interpolate the rotation
        transform.DOLocalRotate(targetRotation, 0.2f);

        // Align the gun's forward direction with the camera's forward direction
        transform.forward = cameraTransform.forward;
    }
}
