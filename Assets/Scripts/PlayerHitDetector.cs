using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitDetector : MonoBehaviour
{

    [SerializeField] private UnityEvent myDeathTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            myDeathTrigger.Invoke();
            playerDeath();
        }
    }

    void playerDeath()
    {
        Time.timeScale = 0.2f;
    }
}