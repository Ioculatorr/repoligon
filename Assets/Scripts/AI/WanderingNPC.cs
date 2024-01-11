using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform targetPoint; // The point the NPC will walk towards

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (targetPoint == null)
        {
            Debug.LogError("Target point is not set for NPC: " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }

    void Update()
    {
        // Check if the NPC has reached the target point
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // You may perform some actions when the NPC reaches the destination
            Debug.Log("NPC reached the destination!");

            // Optionally, you can set a new destination here or disable further movement
            SetNewDestination();
            // DisableMovement();
        }
    }

    void SetDestination()
    {
        if (targetPoint != null)
        {
            navMeshAgent.SetDestination(targetPoint.position);
        }
    }

    // Optional: Set a new destination during runtime
    void SetNewDestination()
    {
        // Example: Set a new random point
        Vector3 randomPoint = Random.insideUnitSphere * 10f;
        randomPoint += targetPoint.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas);
        navMeshAgent.SetDestination(hit.position);
    }

    // Optional: Disable further movement
    void DisableMovement()
    {
        navMeshAgent.isStopped = true;
    }
}

