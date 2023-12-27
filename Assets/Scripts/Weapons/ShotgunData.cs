// ShotgunData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunData", menuName = "Weapons/Shotgun Data")]
public class ShotgunData : BaseWeaponData
{
    public float spreadAngle = 30f;
    public int numberOfPellets = 5;
    [SerializeField] float maxRaycastDistance = 20f;

    public override void Shoot(Transform shootingPoint, Transform shootingPointRaycast)
    {
        // Example shotgun shooting logic using raycast
        for (int i = 0; i < numberOfPellets; i++)
        {
            // Calculate a random direction within the spread angle
            Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f);
            Vector3 shotDirection = spreadRotation * shootingPointRaycast.forward;

            // Raycast in the calculated direction
            Ray ray = new Ray(shootingPointRaycast.position, shotDirection);

            if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance))
            {
                // Handle the hit, apply damage, spawn particle effects, etc.
                Debug.Log($"Shotgun hit {hit.collider.gameObject.name}");

                // Example particle effect for hits
                //SpawnHitParticle(hit.point);

                //// Example particle effect for enemy hits
                //SpawnHitParticleEnemy(hit.point);

                // Example event for hit enemy
                //OnHitEnemy?.Invoke();
            }
            else
            {
                // Handle if the ray doesn't hit anything
                Debug.Log("Shotgun missed!");
            }
        }
    }

    void SpawnHitParticle(Vector3 position)
    {
        // Example: Instantiate the particle effect prefab at the hit position
        //ParticleSystem hitParticle = Instantiate(hitParticlePrefab, position, Quaternion.identity);
        //hitParticle.Emit(1);
        //Destroy(hitParticle.gameObject, hitParticle.main.duration);
    }

    void SpawnHitParticleEnemy(Vector3 position)
    {
        // Example: Instantiate the particle effect prefab at the hit position
        //ParticleSystem hitParticleEnemy = Instantiate(hitParticleEnemyPrefab, position, Quaternion.identity);
        //hitParticleEnemy.Emit(1);
        //Destroy(hitParticleEnemy.gameObject, hitParticleEnemy.main.duration);
    }
}
