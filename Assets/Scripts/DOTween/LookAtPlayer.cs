using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject target;
    [SerializeField, Range(4,20f)] private float sightRange;

    [SerializeField, Range(0.1f, 1f)] private float lookAtSpeed = 1f;

    IEnumerator LookAfterSeconds()
    {
        while (true)
        {
            //if (IsPlayerInSight())
            //{
            //    transform.DOLookAt(player.transform.position, lookAtSpeed);

            //    //this.transform.DOShakeRotation(0.2f, 15f, 2, 10f);

            //}

            target.transform.DOLookAt(player.transform.position, lookAtSpeed);
            yield return new WaitForSeconds(0.2f);
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, sightRange);
    //}

    //bool IsPlayerInSight()
    //{

    //    // Check if the player is within the sight range
    //    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    //    return distanceToPlayer < sightRange;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            StartCoroutine(LookAfterSeconds());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer== 7)
        {
            StopAllCoroutines();
            target.transform.DORotateQuaternion(Quaternion.identity, lookAtSpeed);
        }
    }
}
