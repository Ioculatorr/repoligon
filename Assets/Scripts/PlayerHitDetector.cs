using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitDetector : MonoBehaviour
{
    [SerializeField] private UnityEvent playerDeath;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private float fallDeathSpeed = 50f;

    private bool canDie;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            playerDeath.Invoke();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (playerRB.velocity.magnitude > fallDeathSpeed && canDie == true)
        {
            playerDeath.Invoke();
            canDie = false;
        }
    }
}