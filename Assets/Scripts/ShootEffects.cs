using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootEffects : MonoBehaviour
{
    private AudioSource shootSounds;
    private Transform shootShake;
    private ParticleSystem shootParticle;
    public FloatVariable loadingProgress;

    private void Start()
    {
        shootSounds = GetComponent<AudioSource>();
        shootShake = GetComponent<Transform>();
        shootParticle = GetComponentInChildren<ParticleSystem>();
        SceneLoader.StartedLoading += ShowLoadingProgressBar;
        SceneLoader.ProgressUpdated += UpdateFillAmount;
    }

    public void ShowLoadingProgressBar()
    {

    }

    public void UpdateFillAmount(float currentProgress)
    {

    }

    public void ShootAudio()
    {
        shootSounds.Play();
    }

    public void ShootShake()
    {
        shootShake.transform.DOShakeRotation(1, 3, 10, 15f, true)
        .OnComplete(() =>
        {
            transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
        });
    }
    public void ShootParticle()
    {
        shootParticle.Play();
    }
}
