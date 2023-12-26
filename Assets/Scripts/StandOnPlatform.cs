using Unity.VisualScripting;
using UnityEngine;

public class StandOnPlatform : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        //other.AddComponent<Rigidbody>();
        //other.GetComponent<Rigidbody>().freezeRotation = true;
        //other.GetComponent<Rigidbody>().isKinematic = true;
        //other.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;

        // Set the other as a child of the platform
        player.transform.parent = this.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        player.transform.parent = null;

        //other.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

        // Destroy(other.GetComponent<Rigidbody>());
    }
}
