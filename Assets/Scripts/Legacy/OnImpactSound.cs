using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnImpactSound : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 10)
        {
            AudioClip clip = sounds[UnityEngine.Random.Range(0, sounds.Length)];
            this.GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
}
