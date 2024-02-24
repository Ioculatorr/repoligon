using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitDetector : MonoBehaviour
{
    [SerializeField] private UnityEvent playerDeath;
    [SerializeField] private float fallDeathSpeed = 20f;
    

    //[SerializeField] private Rigidbody playerRB;
    //[SerializeField] private float fallDeathSpeed = 50f;

    //private bool canDie = true;
    //PlayerMovement boolGrounded;

    //[SerializeField] private float minFallDamageVelocity = 9f;
    //private float fallStartY = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            playerDeath.Invoke();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        
        if (collision.relativeVelocity.magnitude >= fallDeathSpeed)
        {
            playerDeath.Invoke();
        }
    }
    //private void OnCollisionEnter(Collision other)
    //{
    //    if (playerRB.velocity.magnitude > fallDeathSpeed && canDie == true)
    //    {
    //        playerDeath.Invoke();
    //        canDie = false;
    //    }
    //}

    //private void Update()
    //{
    //    // If the player starts falling, record the starting Y position
    //    if (boolGrounded.isGrounded == false && Mathf.Approximately(fallStartY, 0f))
    //    {
    //        fallStartY = transform.position.y;
    //    }

    //    // If the player has landed, calculate fall damage
    //    if (boolGrounded.isGrounded == true && !Mathf.Approximately(fallStartY, 0f))
    //    {
    //        float fallDistance = fallStartY - transform.position.y;

    //        if (fallDistance > 0 && fallDistance >= minFallDamageVelocity && canDie == true)
    //        {
    //            playerDeath.Invoke();
    //            canDie = false;
    //        }
    //    }
    //}

}