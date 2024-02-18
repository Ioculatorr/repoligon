using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ElevatorInterface : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Light buttonLight;
    [SerializeField] private AudioClip clickSound;
    
    [SerializeField] private AudioClip elevatorBell;
    
    [SerializeField] private UnityEvent DoorOpen;
    [SerializeField] private UnityEvent DoorClose;

    public static bool isElevating = false;
    
    public void Interact()
    {
        if (isElevating == false)
        {
            DoorClose.Invoke();
            this.GetComponent<AudioSource>().PlayOneShot(clickSound);
            
            isElevating = true;
            buttonLight.enabled = true;
            
            platform.transform.DOMoveY(0f, 10f)
                .SetSpeedBased()
                .SetDelay(1)
                .OnComplete(ElevatorComplete);
        }
    }

    private void ElevatorComplete()
    {
        buttonLight.enabled = false;
        isElevating = false;
        this.GetComponent<AudioSource>().PlayOneShot(elevatorBell);
        
        DoorOpen.Invoke();
    }
}
