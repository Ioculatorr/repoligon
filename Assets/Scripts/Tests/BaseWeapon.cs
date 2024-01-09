using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] internal AudioSource weaponAudio;
    public WeaponData weaponData;
    private Transform shootingPointRaycast;
    [SerializeField] private Transform cameraShake;

    private float currentFireRate;

    public virtual void Awake()
    {
        weaponAudio = GetComponent<AudioSource>();
        gameObject.layer = 12;

    }

    public Transform ShootingPointRaycast
    {
        set
        {
            if(value == null)
                return;
            shootingPointRaycast = value;
        }
    }

    [SerializeField] private UnityEvent onShoot;

    public virtual void Shoot()
    {
        Ray ray = new Ray(shootingPointRaycast.position, shootingPointRaycast.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, weaponData.maxDistance))
        {
            // Access the hit point
            Vector3 hitPoint = hit.point;
            currentFireRate = weaponData.fireRate;

            BulletEmit(hitPoint);

            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object has the EnemyAIWithShooting script
            EnemyAIWithShooting enemyDetection = hitObject.GetComponent<EnemyAIWithShooting>();
            if (enemyDetection != null)
            {
                // Call the OnRaycastHit method on the enemy
                enemyDetection.OnRaycastHit();
                SpawnHitParticleEnemy(hitPoint);
                Debug.Log("Hit: Enemy");
                //OnHitEnemy?.Invoke();
            }
            else
            {
                // Spawn a regular particle effect at the hit point
                SpawnHitParticle(hitPoint);
                Debug.Log("Hit: Non-Enemy");
            }

            onShoot.Invoke();
            PlayPrefabEffects();
        }
        else
        {
            // If the ray doesn't hit anything, consider it as hitting the skybox
            Debug.Log("Hit: Nothing!");

            // Invoke the shoot trigger event
            onShoot.Invoke();
            PlayPrefabEffects();
        }

        // Visualize the ray in the Scene view (for debugging purposes)
        Debug.DrawRay(ray.origin, ray.direction * weaponData.maxDistance, Color.red, 0.1f);
    }

    private void Update()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    public virtual bool CanShoot()
    {
        return currentFireRate <= 0;
    }
    
    internal void ShootShake()
    {
        cameraShake.transform.DOShakeRotation(0.5f, 1.5f, 6, 15f, true)
            .OnComplete(() =>
            {
                cameraShake.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
            });
    }

    private void DisableLight()
    {
        GetComponentInChildren<Light>().intensity = 0f;
    }

    internal void ShootShakeGun()
    {
        transform.DOPunchRotation(new Vector3(-15f, 0, 0), 0.5f, 10, 0f)
            .OnComplete(() =>
            {
                transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
            });
    }


    public abstract void BulletEmit(Vector3 hitPoint);
    public abstract void SpawnHitParticleEnemy(Vector3 hitPoint);
    public abstract void SpawnHitParticle(Vector3 hitPoint);
    public abstract void PlayPrefabEffects();

}
