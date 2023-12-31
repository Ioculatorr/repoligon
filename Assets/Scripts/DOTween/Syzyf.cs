using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syzyf : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float totalDuration = 10f; // Total duration for the entire path
    [SerializeField] private float snapSpeed = 1f;

    void Start()
    {
        // Create a sequence for the path animation
        Sequence pathSequence = DOTween.Sequence();

        // Add waypoints to the sequence
        foreach (Transform waypoint in waypoints)
        {
            float segmentDuration = totalDuration / waypoints.Length;

            // Move to the waypoint with a segment duration
            pathSequence.Append(transform.DOMove(waypoint.position, segmentDuration).SetEase(Ease.Linear));

            // Rotate to face the next waypoint with a segment duration
            pathSequence.Join(transform.DOLookAt(waypoint.position, snapSpeed).SetEase(Ease.Linear));
        }

        // Set the loop type (optional)
        pathSequence.SetLoops(-1, LoopType.Restart);
    }
}
