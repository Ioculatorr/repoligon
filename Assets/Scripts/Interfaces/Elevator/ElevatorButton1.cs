using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorButton1 : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Light light;
    [SerializeField] private AudioClip clickSound;
    

    public void Interact()
    {
        if (ElevatorInterface.isElevating == false)
        {
            this.GetComponent<AudioSource>().PlayOneShot(clickSound);
            ElevatorInterface.isElevating = true;
            light.enabled = true;
            platform.transform.DOMoveY(0f, 5f)
                .SetDelay(1)
                .OnComplete(ElevatorComplete);
        }
    }

    private void ElevatorComplete()
    {
        light.enabled = false;
        ElevatorInterface.isElevating = false;
        
    }
}

