using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;
using DG.Tweening;
using Unity.VisualScripting;
using static Dreamteck.Splines.ParticleController;

public class SimpleShooting : MonoBehaviour
{
    //[SerializeField] private GameObject bulletPrefab;

    //[SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform shootingPointRaycast;
    [SerializeField] private Transform cameraShake;
    [SerializeField] private AudioSource weaponAudio;

    //[SerializeField] private float bulletSpeed = 100f;
    private float currentFireRate; // bullets per second


    [SerializeField] private ParticleSystem hitParticlePrefab;
    [SerializeField] private ParticleSystem hitParticleEnemyPrefab;
    [SerializeField] private ParticleSystem bulletParticle;

    private float nextFireTime = 0f;


    [Header("Weapons")]


    [SerializeField] private WeaponData scriptableObjectA;
    [SerializeField] private WeaponData scriptableObjectB;

    private WeaponData currentScriptableObject;
    private GameObject spawnedPrefab;

    private Tween gunShakeTween;











    [SerializeField] private UnityEvent onShoot;
    
    
    [SerializeField] private UnityEvent KilledYourself;
    [SerializeField] private AudioClip suicideSound;
    bool AimAtYourself = false;
    
    public static Action OnHitEnemy;

    private bool PickedUpSmth = false;


    void Start()
    {
        // Initialize with the first ScriptableObject
        SetScriptableObject(scriptableObjectA);
    }

    void Update()
    {
        // Check for user input (e.g., space key) to trigger shooting
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime && !PickedUpSmth)
        {
            Shoot();
            nextFireTime = Time.time + 1f / currentFireRate;
        }
        // Check for input or any condition to switch between ScriptableObjects
        if (Input.GetKeyDown(KeyCode.Q)&& !PickedUpSmth)
        {
            // Toggle between the two ScriptableObjects
            ToggleScriptableObject();
        }
        if (Input.GetKeyDown(KeyCode.R) && !PickedUpSmth)
        {
            // Kill yourself
            LifeRestart();
        }
    }

    //void Shoot()
    //{
    //    // Create a new bullet instance
    //    GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

    //    // Access the Rigidbody component of the bullet
    //    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

    //    // Apply force to the bullet to make it move
    //    bulletRb.velocity = shootingPoint.forward * bulletSpeed;

    //    myShootTrigger.Invoke();



    //    // Destroy the bullet after a certain time (adjust as needed)
    //    Destroy(bullet, 2f);

    //    StartCoroutine(CheckBulletVelocity(bullet));
    //}

    //IEnumerator CheckBulletVelocity(GameObject bullet)
    //{
    //    // Wait for a short delay (adjust as needed)
    //    yield return new WaitForSeconds(0.05f);

    //    // Check if the magnitude of the velocity is less than a certain threshold
    //    if (bullet.GetComponent<Rigidbody>().velocity.magnitude < 90f)
    //    {
    //        // Destroy the bullet gameObject
    //        Destroy(bullet);
    //    }
    //}

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && AimAtYourself == true)
        {
            KilledYourself.Invoke();
        }
        
        
        
        
        
        // Cast a ray from the shooting point forward
        Ray ray = new Ray(shootingPointRaycast.position, shootingPointRaycast.forward);

        // Use a large distance for the raycast (adjust as needed)
        float maxRaycastDistance = currentScriptableObject.maxDistance;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance))
        {
            // Access the hit point
            Vector3 hitPoint = hit.point;

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
        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red, 0.1f);
    }

    void SpawnHitParticle(Vector3 position)
    {
        // Instantiate the particle effect prefab at the hit position
        ParticleSystem hitParticle = Instantiate(hitParticlePrefab, position, Quaternion.identity);

        // Play the particle effect
        hitParticle.Emit(1);

        // Destroy the particle effect after its duration (adjust as needed)
        Destroy(hitParticle.gameObject, hitParticle.main.duration);
    }

    void SpawnHitParticleEnemy(Vector3 position)
    {
        // Instantiate the particle effect prefab at the hit position
        ParticleSystem hitParticleEnemy = Instantiate(hitParticleEnemyPrefab, position, Quaternion.identity);

        // Play the particle effect
        hitParticleEnemy.Emit(1);

        // Destroy the particle effect after its duration (adjust as needed)
        Destroy(hitParticleEnemy.gameObject, hitParticleEnemy.main.duration);
    }

    void ToggleScriptableObject()
    {
        // Destroy the current prefab if it exists
        DestroySpawnedPrefab();

        // Switch between ScriptableObjects
        if (currentScriptableObject == scriptableObjectA)
        {
            SetScriptableObject(scriptableObjectB);
        }
        else
        {
            SetScriptableObject(scriptableObjectA);
        }
    }

    void SetScriptableObject(WeaponData newScriptableObject)
    {
        // Set the current ScriptableObject and spawn its associated prefab
        currentScriptableObject = newScriptableObject;

        // For demonstration purposes, you can print some information
        Debug.Log($"Switched to {currentScriptableObject.name}");

        currentFireRate = currentScriptableObject.fireRate;




        // Spawn the associated prefab
        spawnedPrefab = Instantiate(currentScriptableObject.weaponPrefab, transform.position, transform.rotation);






        spawnedPrefab.transform.parent = transform;
        spawnedPrefab.layer = 12;
    }

    void DestroySpawnedPrefab()
    {
        // Destroy the previously spawned prefab if it exists
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
        }
    }

    void PlayPrefabEffects()
    {
        weaponAudio.clip = currentScriptableObject.audio;
        weaponAudio.Play();

        //spawnedPrefab.GetComponentInChildren<AudioSource>().Play();
        spawnedPrefab.GetComponentInChildren<ParticleSystem>().Emit(1);
        spawnedPrefab.GetComponentInChildren<Light>().intensity = Mathf.RoundToInt(UnityEngine.Random.Range(2.5f, 5f));
        spawnedPrefab.GetComponentInChildren<Light>().range = Mathf.RoundToInt(UnityEngine.Random.Range(2.5f, 5f));

        Invoke("DisableLight", 0.05f);

        ShootShake();
        ShootShakeGun();
    }

    private void ShootShake()
    {
                cameraShake.transform.DOShakeRotation(0.5f, 1.5f, 6, 15f, true)
        .OnComplete(() =>
        {
            cameraShake.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
        });
    }

    private void DisableLight()
    {
        spawnedPrefab.GetComponentInChildren<Light>().intensity = 0f;
    }

    private void ShootShakeGun()
    {
                spawnedPrefab.transform.DOPunchRotation(new Vector3(-15f, 0, 0), 0.5f, 10, 0f)
        .OnComplete(() =>
        {
        spawnedPrefab.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f);
        });
    }

    private void BulletEmit(Vector3 hitPoint)
    {
        // Get the direction from the shooting point to the hit point
        Vector3 direction = (hitPoint - bulletParticle.transform.position).normalized;

        // Set the rotation of the particle system towards the hit point
        bulletParticle.transform.rotation = Quaternion.LookRotation(direction);

        // Play the particle effect
        bulletParticle.Emit(1);
    }

    private void LifeRestart()
    {
        AimAtYourself = true;
        this.AddComponent<AudioSource>().PlayOneShot(suicideSound);
        Destroy(this.GetComponent<AudioSource>(), suicideSound.length);
        spawnedPrefab.transform.DOLookAt(cameraShake.transform.position, 0.2f);
        
    }

    public void HideWeaponOnPickUp()
    {
        PickedUpSmth = true;
        DestroySpawnedPrefab();
    }
    
    public void SpawnWeaponOnDropOff()
    {
        PickedUpSmth = false;
        SetScriptableObject(scriptableObjectA);
    }
}
