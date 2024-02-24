using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ElevatorInterface : MonoBehaviour, IInteractable
{
    [SerializeField] private Light buttonLight;
    [SerializeField] private AudioClip clickSound;
    
    [SerializeField] private UnityEvent Floor1;

    public static bool isElevating = false;
    
    public void Interact()
    {
        this.GetComponent<AudioSource>().PlayOneShot(clickSound);
        buttonLight.enabled = true;
        
        Floor1.Invoke();
    }

    public void TurnOff()
    {
        buttonLight.enabled = false;
    }
}
