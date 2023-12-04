using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float fireRate = 10f; // bullets per second

    private float nextFireTime = 0f;

    [SerializeField] private UnityEvent myShootTrigger;

    void Update()
    {
        // Check for user input (e.g., space key) to trigger shooting
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        // Create a new bullet instance
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        // Access the Rigidbody component of the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Apply force to the bullet to make it move
        bulletRb.velocity = shootingPoint.forward * bulletSpeed;

        myShootTrigger.Invoke();



        // Destroy the bullet after a certain time (adjust as needed)
        Destroy(bullet, 2f);

        StartCoroutine(CheckBulletVelocity(bullet));
    }

    IEnumerator CheckBulletVelocity(GameObject bullet)
    {
        // Wait for a short delay (adjust as needed)
        yield return new WaitForSeconds(0.05f);

        // Check if the magnitude of the velocity is less than a certain threshold
        if (bullet.GetComponent<Rigidbody>().velocity.magnitude < 90f)
        {
            // Destroy the bullet gameObject
            Destroy(bullet);
        }
    }
}
