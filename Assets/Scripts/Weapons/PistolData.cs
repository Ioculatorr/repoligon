// PistolData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "PistolData", menuName = "Weapons/Pistol Data")]
public class PistolData : BaseWeaponData
{
    [SerializeField] float maxRaycastDistance = 100f;

    public override void Shoot(Transform shootingPoint, Transform shootingPointRaycast)
    {
        // Example pistol shooting logic using raycast
        Ray ray = new Ray(shootingPointRaycast.position, shootingPointRaycast.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance))
        {
            // Handle the hit, apply damage, spawn particle effects, etc.
            Debug.Log($"Pistol hit {hit.collider.gameObject.name}");

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
            Debug.Log("Pistol missed!");
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
