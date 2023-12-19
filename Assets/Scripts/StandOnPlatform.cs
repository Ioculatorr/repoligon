using Unity.VisualScripting;
using UnityEngine;

public class StandOnPlatform : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            // Set the player as a child of the platform
            player.transform.parent = transform;
            player.AddComponent<Rigidbody>();
            player.GetComponent<Rigidbody>().freezeRotation = true;
            Debug.Log("I am in");
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            // Remove the player as a child of the platform
            player.transform.parent = null;
            Destroy(player.GetComponent<Rigidbody>());

            Debug.Log("I am out");
        }
    }
}
