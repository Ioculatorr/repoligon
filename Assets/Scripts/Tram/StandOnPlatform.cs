using Unity.VisualScripting;
using UnityEngine;

public class StandOnPlatform : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private bool canJump;


    private void OnTriggerEnter(Collider otherCollider)
    {
        
        otherCollider.transform.parent = transform;
        
        // Check if the collider entering the trigger is the player
        if (otherCollider.gameObject == player)
        {
            player.transform.parent = transform;

            Debug.Log("Player entered the trigger");
        }
        else if (otherCollider.gameObject.layer == 14)
        {
            //otherCollider.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        otherCollider.transform.parent = null;
        
        if (otherCollider.gameObject == player)
        {
            player.transform.parent = null;
            

            Debug.Log("I am out");
        }
        else if (otherCollider.gameObject.layer == 14)
        {
            //otherCollider.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}