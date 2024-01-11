using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class Pistol : BaseWeapon
{
    [SerializeField] private ParticleSystem hitParticlePrefab;
    [SerializeField] private ParticleSystem hitParticleEnemyPrefab;
    [SerializeField] private ParticleSystem bulletParticle;
    
    
    [SerializeField] private Light gunfireLight;
    
    
    public override void BulletEmit(Vector3 hitPoint)
    {
        // Get the direction from the shooting point to the hit point
        Vector3 direction = (hitPoint - bulletParticle.transform.position).normalized;

        // Set the rotation of the particle system towards the hit point
        bulletParticle.transform.rotation = Quaternion.LookRotation(direction);

        // Play the particle effect
        bulletParticle.Emit(1);
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
        // Instantiate the particle effect prefab at the hit position
        ParticleSystem hitParticle = Instantiate(hitParticlePrefab, hitPoint, Quaternion.identity);

        // Play the particle effect
        hitParticle.Emit(1);

        // Destroy the particle effect after its duration (adjust as needed)
        Destroy(hitParticle.gameObject, hitParticle.main.duration);
    }

    public override void PlayPrefabEffects()
    {
        weaponAudio.clip = weaponData.audio;
        weaponAudio.Play();

        //spawnedPrefab.GetComponentInChildren<AudioSource>().Play();
        //this.GetComponentInChildren<ParticleSystem>().Emit(1);

        StartCoroutine(GunfireFlash());
        
        
        ShootShake();
        ShootShakeGun();
    }
    

    IEnumerator GunfireFlash()
    {
        gunfireLight.intensity = Mathf.RoundToInt(UnityEngine.Random.Range(2.5f, 5f));
        gunfireLight.range = Mathf.RoundToInt(UnityEngine.Random.Range(2.5f, 5f));
        
        yield return new WaitForSeconds((0.05f));
        gunfireLight.intensity = 0f;
    }
}
