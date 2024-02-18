using Unity.VisualScripting;
using UnityEngine;

public class StandOnPlatform : MonoBehaviour
{
    [SerializeField] private GameObject player;


    private void OnTriggerEnter(Collider otherCollider)
    {
        // Check if the collider entering the trigger is the player
        if (otherCollider.gameObject == player)
        {
            // Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
            //
            // // Check if the player already has a Rigidbody
            // if (playerRigidbody == null)
            // {
            //     player.AddComponent<Rigidbody>();
            //     playerRigidbody = player.GetComponent<Rigidbody>();
            //     playerRigidbody.freezeRotation = true;
            //     playerRigidbody.isKinematic = true;
            // }

            // Set the player as a child of the platform
            player.transform.parent = transform;
            Debug.Log("Player entered the trigger");
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.gameObject == player)
        {
            // Destroy(player.GetComponent<Rigidbody>());
            //
            // // Remove the other as a child of the platform
            player.transform.parent = null;

            Debug.Log("I am out");
        }
    }
}