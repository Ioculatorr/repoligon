using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffects : MonoBehaviour
{
    private AudioSource deathSounds;
    [SerializeField] private CanvasGroup deathCanvas;

    private bool canDie = true;

    private void Start()
    {
        deathSounds = GetComponent<AudioSource>();
    }

    public void deathEffects()
    {
        if(canDie)
        {
            deathSounds.Play();
            deathCanvas.alpha = 1.0f;
            canDie = false;
        }
    }

    //public void deathSound()
    //{
    //    deathSounds.Play();
    //}

    //public void deathScreen()
    //{
    //    deathCanvas.alpha = 1.0f;
    //}
}
