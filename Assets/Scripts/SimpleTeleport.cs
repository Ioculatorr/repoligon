using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTeleport : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform target;

    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = target.transform.position;
    }
}
