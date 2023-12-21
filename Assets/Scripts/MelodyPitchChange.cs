using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyPitchChange : MonoBehaviour
{
    private void Start()
    {
        PitchChange1();
    }

    private void PitchChange1()
    {
        this.gameObject.GetComponent<AudioSource>().DOPitch(Random.Range(0.3f, 0.7f), Random.Range(1f, 3f)).OnComplete(PitchChange2);
    }

    private void PitchChange2()
    {
        this.gameObject.GetComponent<AudioSource>().DOPitch(Random.Range(0.3f, 0.7f), Random.Range(1f, 3f)).OnComplete(PitchChange1);
    }
}
