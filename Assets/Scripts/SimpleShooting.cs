using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleShooting : MonoBehaviour
{
    //[SerializeField] private GameObject bulletPrefab;

    //[SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform shootingPointRaycast;

    //[SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float fireRate = 10f; // bullets per second


    [SerializeField] private ParticleSystem hitParticlePrefab;
    [SerializeField] private ParticleSystem hitParticleEnemyPrefab;

    private float nextFireTime = 0f;

    [SerializeField] private UnityEvent onShoot;

    public static Action OnHitEnemy;

    void Update()
    {
        // Check for user input (e.g., space key) to trigger shooting
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
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
        // Cast a ray from the shooting point forward
        Ray ray = new Ray(shootingPointRaycast.position, shootingPointRaycast.forward);

        // Use a large distance for the raycast (adjust as needed)
        float maxRaycastDistance = 100f;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance))
        {
            // Access the hit point
            Vector3 hitPoint = hit.point;

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
        }
        else
        {
            // If the ray doesn't hit anything, consider it as hitting the skybox
            Debug.Log("Hit: Nothing!");

            // Invoke the shoot trigger event
            onShoot.Invoke();
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
}
