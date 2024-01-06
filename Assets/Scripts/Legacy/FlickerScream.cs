using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerScream : MonoBehaviour
{
    private bool isPlaying = false;
    private float timeDelay;
    [SerializeField] AudioClip[] sounds;
    [SerializeField] private Transform _camera;

    void Update()
    {
        if (isPlaying == false)
        {
            StartCoroutine(FlickeringAudioSource());
        }
    }

    public void MySoundShake()
    {
        this.transform.DOShakePosition(1f, 0.1f, 20, 90);
    }
    public void MyCameraShake()
    {
        _camera.DOShakeRotation(2f, 1f, 40);
    }

    IEnumerator FlickeringAudioSource()
    {
        isPlaying = true;

        AudioClip clip = sounds[Random.Range(0, sounds.Length)];
        this.GetComponent<AudioSource>().PlayOneShot(clip);
        timeDelay = Random.Range(1f, 8f);
        Invoke("MyCameraShake", 0.0f);
        yield return new WaitForSeconds(timeDelay);


        //this.gameObject.GetComponent<AudioSource>().Stop();
        //timeDelay = Random.Range(10f, 20f);
        //yield return new WaitForSeconds(timeDelay);

        isPlaying = false;

    }
}
