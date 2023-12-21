using Unity.VisualScripting;
using UnityEngine;

public class StandOnPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the other as a child of the platform
            other.transform.parent = transform;
            other.AddComponent<Rigidbody>();
            other.GetComponent<Rigidbody>().freezeRotation = true;
            other.GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("I am in");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Remove the other as a child of the platform
            other.transform.parent = null;
            Destroy(other.GetComponent<Rigidbody>());

            Debug.Log("I am out");
        }
    }
}
