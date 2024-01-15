using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInterface : MonoBehaviour, IInteractable
{
    [SerializeField] private Light lampLight;
    private bool isAction1Active = false;

    public void Interact()
    {
        // Toggle between different actions
        if (isAction1Active)
        {
            // Execute Action 2
            lampLight.enabled = true;
        }
        else
        {
            // Execute Action 1
            lampLight.enabled = false;
        }

        // Toggle the flag
        isAction1Active = !isAction1Active;
    }
}
