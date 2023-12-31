using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyStalker : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent agent;
    [SerializeField] private float desiredDistance = 5f;
    [SerializeField] private float sightRange = 10f;

    // Other variables...

    void Start()
    {
        // Initialize variables...
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (IsPlayerInSight())
        {
            // Calculate the desired position for the NPC to maintain distance
            Vector3 desiredPosition = player.position - (player.position - transform.position).normalized * desiredDistance;

            // Set the destination to the desired position
            agent.destination = desiredPosition;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Use Gizmos to visualize the sight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    bool IsPlayerInSight()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer < sightRange;
    }

}
