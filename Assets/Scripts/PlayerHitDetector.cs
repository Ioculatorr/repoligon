using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitDetector : MonoBehaviour
{
    [SerializeField] private UnityEvent playerDeath;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float fallDeathSpeed = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            playerDeath.Invoke();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (characterController.velocity.magnitude > fallDeathSpeed)
        {
            playerDeath.Invoke();
        }
    }
}