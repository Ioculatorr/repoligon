using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootEffects : MonoBehaviour
{
    private AudioSource shootSounds;
    private Transform shootShake;
    private Light shootLight;
    private ParticleSystem shootParticle;

    [SerializeField] private GameObject gun;
    [SerializeField] private ParticleSystem shootParticleBullet;

    private void Start()
    {
        shootSounds = GetComponent<AudioSource>();
        shootShake = GetComponent<Transform>();
        shootLight = GetComponentInChildren<Light>();
    }

    public void ShootAudio()
    {
        shootSounds.Play();
    }

    public void ShootShake()
    {
        shootShake.transform.DOShakeRotation(0.5f, 1.5f, 6, 15f, true)
        .OnComplete(() =>
        {
            transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
        });
    }

    public void ShootShakeGun()
    {
        gun.gameObject.transform.DOPunchRotation(new Vector3(-15f, 0, 0), 0.5f, 10, 0f);
        gun.gameObject.transform.DOPunchPosition(new Vector3(0, 0, -0.1f), 0.5f, 10, 0f, false)
        .OnComplete(() =>
        {
            gun.gameObject.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);
            gun.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
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
