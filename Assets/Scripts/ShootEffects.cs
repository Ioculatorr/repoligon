using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootEffects : MonoBehaviour
{
    private AudioSource shootSounds;
    private Transform shootShake;
    private ParticleSystem shootParticle;
    private Light shootLight;

    [SerializeField] private GameObject gun;
    [SerializeField] private ParticleSystem shootParticleBullet;

    private void Start()
    {
        shootSounds = GetComponent<AudioSource>();
        shootShake = GetComponent<Transform>();
        shootLight = GetComponentInChildren<Light>();
        shootParticle = GetComponentInChildren<ParticleSystem>();
    }

    public void ShootAudio()
    {
        shootSounds.Play();
    }

    public void ShootShake()
    {
        shootShake.transform.DOShakeRotation(1, 3f, 10, 15f, true)
        .OnComplete(() =>
        {
            transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
        });
    }

    public void ShootShakeGun()
    {
        gun.gameObject.transform.DOShakeRotation(1f, new Vector3(3f, 0f, 0f), 10, 15f, true)
        .OnComplete(() =>
        {
            gun.gameObject.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
        });
    }
    public void ShootParticle()
    {
        shootParticle.Emit(1);
    }

    public void ShootParticleBullet()
    {
        shootParticleBullet.GetComponent<ParticleSystem>().Emit(1);
    }

    public void ShootLight()
    {
        shootLight.intensity = Mathf.RoundToInt(Random.Range(2.5f, 5f));
        shootLight.range = Mathf.RoundToInt(Random.Range(2.5f, 5f));

        Invoke("DisableLight", 0.05f);
    }

    private void DisableLight()
    {
        shootLight.intensity = 0f;
    }
}
