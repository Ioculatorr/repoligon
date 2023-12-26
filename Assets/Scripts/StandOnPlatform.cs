using Unity.VisualScripting;
using UnityEngine;

public class StandOnPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.AddComponent<Rigidbody>();
            //other.GetComponent<Rigidbody>().freezeRotation = true;
            other.GetComponent<Rigidbody>().isKinematic = true;

            // Set the other as a child of the platform
            other.transform.parent = transform;
            Debug.Log("I am in");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Destroy(other.GetComponent<Rigidbody>());

            // Remove the other as a child of the platform
            other.transform.parent = null;

            Debug.Log("I am out");
        }
    }
}
