using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAIWithShooting : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float sightRange = 10f;

    [SerializeField] private AudioClip enemyDeathSound;

    void Start()
    {
        //SimpleShooting.OnHitEnemy += Die;

        agent = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine(ShootAtPlayer());
    }

    void Update()
    {
        if(IsPlayerInSight())
        {
            agent.destination = player.position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Use Gizmos to visualize the sight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            // Check if player is in sight
            if (IsPlayerInSight())
            {
                // Shoot at the player
                Shoot();
            }

            yield return new WaitForSeconds(1f / fireRate);
        }
    }

    void Shoot()
    {
        // Calculate the direction from the enemy to the player
        Vector3 shootingDirection = (player.position - shootingPoint.position).normalized;

        // Create a new bullet instance
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.LookRotation(shootingDirection));

        // Access the Rigidbody component of the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Apply force to the bullet to make it move
        bulletRb.velocity = shootingDirection * bulletSpeed;

        Destroy(bullet, 2f);
    }


    bool IsPlayerInSight()
    {

        // Check if the player is within the sight range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer < sightRange;
    }

    public void OnRaycastHit()
    {
        Die();
    }

    void Die()
    {
        // Play death sound from enemy's position
        AudioSource.PlayClipAtPoint(enemyDeathSound, transform.position);

        // Instantiate a temporary object to handle the sound
        GameObject tempObject = new GameObject("TempObject");
        tempObject.transform.position = transform.position;

        // Play sound on the temporary object
        AudioSource tempAudio = tempObject.AddComponent<AudioSource>();
        tempAudio.clip = enemyDeathSound;
        tempAudio.Play();


        // Destroy the temporary object after the sound duration
        Destroy(tempObject, enemyDeathSound.length);

        // Destroy the enemy
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.layer == 9)
    //    {
    //        this.gameObject.GetComponent<AudioSource>().Play();
    //        //Destroy(gameObject);
    //    }
    //}
}
