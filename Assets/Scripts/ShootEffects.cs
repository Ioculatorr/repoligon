using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootEffects : MonoBehaviour
{
    [SerializeField] private AudioSource shootSounds;
    [SerializeField] private Light shootLight;

    [SerializeField] private Transform shootShake;

    [SerializeField] private ParticleSystem shootParticle;
    [SerializeField] private ParticleSystem shootParticleBullet;

    [SerializeField] private GameObject gun;

    private Tween gunShakeTween;

    public void ShootEffectsRifle()
    {
        ShootAudio();
        ShootLight();
        ShootShake();
        ShootShakeGun();
        ShootParticle();
        ShootParticleBullet();
    }

    private void ShootAudio()
    {
        shootSounds.Play();
    }

    private void ShootShake()
    {
        shootShake.transform.DOShakeRotation(0.5f, 1.5f, 6, 15f, true)
        .OnComplete(() =>
        {
            transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
        });
    }

    private void ShootShakeGun()
    {
        // Ensure the previous tween is killed before starting a new one
        if (gunShakeTween != null && gunShakeTween.IsActive())
        {
            gunShakeTween.Kill();
        }

        // Create a new reusable tween instance
        gunShakeTween = DOTween.Sequence()
            .Append(gun.gameObject.transform.DOPunchRotation(new Vector3(-15f, 0, 0), 0.5f, 10, 0f))
            .Append(gun.gameObject.transform.DOPunchPosition(new Vector3(0, 0, -0.1f), 0.5f, 10, 0f, false))
            .OnComplete(() =>
            {
                // Reset the gun position and rotation
                gun.gameObject.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);
                gun.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
            });

        // Set the reuse option for the tween
        gunShakeTween.SetRecyclable(true);
        gunShakeTween.SetAutoKill(false);

        // Start the tween
        gunShakeTween.Restart();
    }
    private void ShootParticle()
    {
        shootParticle.Emit(1);
    }

    private void ShootParticleBullet()
    {
        shootParticleBullet.GetComponent<ParticleSystem>().Emit(1);
    }

    private void ShootLight()
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
