using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LookAtOutsideVision : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cameraFOV;
    //[SerializeField] private float playerFieldOfVision = 90f;
    [SerializeField, Range(4,20f)] private float sightRange;

    [SerializeField, Range(0.1f, 1f)] private float lookAtSpeed = 1f;

    void Update()
    {
        // Check if the object is within the camera's field of view
        if (!IsObjectInCameraView())
        {
            transform.DOLookAt(player.transform.position, lookAtSpeed);
            //StartCoroutine(LookAfterSeconds());
        }
    }

    IEnumerator LookAfterSeconds()
    {
        while (true)
        {
            //if (IsPlayerInSight())
            //{
            //    transform.DOLookAt(player.transform.position, lookAtSpeed);

            //    //this.transform.DOShakeRotation(0.2f, 15f, 2, 10f);

            //}
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    bool IsPlayerInSight()
    {

        // Check if the player is within the sight range
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        return distanceToPlayer < sightRange;
    }

    bool IsObjectInCameraView()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cameraFOV);

        // Check if the object is within the camera's frustum
        return GeometryUtility.TestPlanesAABB(planes, this.GetComponent<Renderer>().bounds);
    }
}
