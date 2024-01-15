using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private float interactionRange = 3f;
    
    private void Update()
    {
        // Check for player input, e.g., "E" key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Perform raycast to detect interactable objects
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            // Check if the hit object implements the IInteractable interface
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Call the Interact method on the interactable object
                interactable.Interact();
            }
        }
    }
}

