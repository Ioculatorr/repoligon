using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class TramDOTween : MonoBehaviour
{
    public Transform[] waypoints; // Reference to the waypoints attached to the platform
    public float duration = 5f;   // Duration of the movement along the path

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartSplineMovement();
        }
    }

    private void OnDrawGizmos()
    {
        // Draw lines between waypoints in the Scene view
        DrawWaypointLines();
    }

    private void StartSplineMovement()
    {
        // Create a path using the waypoints
        Vector3[] path = new Vector3[waypoints.Length + 1]; // Increased array size by 1
        for (int i = 0; i < waypoints.Length; i++)
        {
            path[i] = waypoints[i].position;
        }

        // Add the first waypoint at the end to close the path
        path[path.Length - 1] = waypoints[0].position;

        // Move the platform along the path using DOTween
        transform.DOPath(path, duration, PathType.CatmullRom)
            .SetOptions(true, AxisConstraint.Y)
            .SetLookAt(0.001f);
            //.SetEase(Ease.Linear)
            //.SetLoops(-1, LoopType.Yoyo);
        //.OnComplete(OnPathComplete); // You can add a callback for completion if needed
    }

    private void OnPathComplete()
    {
        // Called when the platform completes the path
        Debug.Log("Platform reached the end of the path");
    }

    private void DrawWaypointLines()
    {
        Handles.color = Color.yellow;

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Handles.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }

        // Connect the last waypoint to the first one to close the path
        //if (waypoints.Length > 1)
        //{
        //    Handles.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
        //}
    }
}
