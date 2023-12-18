using UnityEngine;

public class StandOnPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the player as a child of the platform
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Remove the player as a child of the platform
            other.transform.parent = null;
        }
    }
}
