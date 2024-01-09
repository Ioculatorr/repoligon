using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class Pistol : BaseWeapon
{
    [SerializeField] private ParticleSystem hitParticlePrefab;
    [SerializeField] private ParticleSystem hitParticleEnemyPrefab;
    
    public override void BulletEmit(Vector3 hitPoint)
    {
        // Instantiate the particle effect prefab at the hit position
        ParticleSystem hitParticle = Instantiate(hitParticlePrefab, hitPoint, Quaternion.identity);

        // Play the particle effect
        hitParticle.Emit(1);

        // Destroy the particle effect after its duration (adjust as needed)
        Destroy(hitParticle.gameObject, hitParticle.main.duration);
    }

    public override void SpawnHitParticleEnemy(Vector3 hitPoint)
    {
        // Instantiate the particle effect prefab at the hit position
        ParticleSystem hitParticleEnemy = Instantiate(hitParticleEnemyPrefab, hitPoint, Quaternion.identity);

        // Play the particle effect
        hitParticleEnemy.Emit(1);

        // Destroy the particle effect after its duration (adjust as needed)
        Destroy(hitParticleEnemy.gameObject, hitParticleEnemy.main.duration);
    }

    public override void SpawnHitParticle(Vector3 hitPoint)
    {
    }

    public override void PlayPrefabEffects()
    {
        weaponAudio.clip = weaponData.audio;
        weaponAudio.Play();

        //spawnedPrefab.GetComponentInChildren<AudioSource>().Play();
        this.GetComponentInChildren<ParticleSystem>().Emit(1);
        this.GetComponentInChildren<Light>().intensity = Mathf.RoundToInt(UnityEngine.Random.Range(2.5f, 5f));
        this.GetComponentInChildren<Light>().range = Mathf.RoundToInt(UnityEngine.Random.Range(2.5f, 5f));

        Invoke("DisableLight", 0.05f);

        ShootShake();
        ShootShakeGun();
    }
}