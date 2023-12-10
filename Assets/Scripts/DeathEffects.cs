using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffects : MonoBehaviour
{
    private AudioSource deathSounds;

    private void Start()
    {
        deathSounds = GetComponent<AudioSource>();
    }

    public void deathSound()
    {
        deathSounds.Play();
    }
}
