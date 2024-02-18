using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ElevatorButton1 : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Light buttonLight;
    [SerializeField] private AudioClip clickSound;
    
    [SerializeField] private AudioClip elevatorBell;
    [SerializeField] private UnityEvent DoorOpen;
    [SerializeField] private UnityEvent DoorClose;
    
    
    public void Interact()
    {
        if (ElevatorInterface.isElevating == false)
        {
            DoorClose.Invoke();
            this.GetComponent<AudioSource>().PlayOneShot(clickSound);
            ElevatorInterface.isElevating = true;
            buttonLight.enabled = true;
            
            platform.transform.DOMoveY(-59f, 10f)
                .SetSpeedBased()
                .SetDelay(1)
                .OnComplete(ElevatorComplete);
        }
    }

    private void ElevatorComplete()
    {
        buttonLight.enabled = false;
        ElevatorInterface.isElevating = false;
        this.GetComponent<AudioSource>().PlayOneShot(elevatorBell);
        
        DoorOpen.Invoke();
    }
}

