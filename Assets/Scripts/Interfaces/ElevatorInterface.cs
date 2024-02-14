using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorInterface : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Light light;
    [SerializeField] private AudioClip clickSound;

    public static bool isElevating = false;
    
    public void Interact()
    {
        if (isElevating == false)
        {
            this.GetComponent<AudioSource>().PlayOneShot(clickSound);
            isElevating = true;
            light.enabled = true;
            platform.transform.DOMoveY(10f, 5f)
                .SetDelay(1)
                .OnComplete(ElevatorComplete);
        }
    }

    private void ElevatorComplete()
    {
        light.enabled = false;
        isElevating = false;
    }
}
