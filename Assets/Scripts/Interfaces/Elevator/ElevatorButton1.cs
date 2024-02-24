using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ElevatorButton1 : MonoBehaviour, IInteractable
{
    [SerializeField] private Light buttonLight;
    [SerializeField] private AudioClip clickSound;
    
    [SerializeField] private UnityEvent Floor0;
    
    
    public void Interact()
    {
        this.GetComponent<AudioSource>().PlayOneShot(clickSound);
        buttonLight.enabled = true;
        
        Floor0.Invoke();
    }
    
    public void TurnOff()
    {
        buttonLight.enabled = false;
    }
}

