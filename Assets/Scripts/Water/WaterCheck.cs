using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WaterCheck : MonoBehaviour
{
    [SerializeField] private UnityEvent InWater;
    [SerializeField] private UnityEvent OutWater;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Rigidbody>().useGravity = false;

            if (other.CompareTag("Player"))
            {
                InWater.Invoke();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Rigidbody>().useGravity = true;
            
            if (other.CompareTag("Player"))
            {
                OutWater.Invoke();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * 9.8f, ForceMode.Acceleration); // Adjust the force as needed
        }
    }
}
