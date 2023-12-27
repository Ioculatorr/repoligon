// BaseWeaponData.cs
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseWeaponData", menuName = "Weapons/Base Weapon Data")]
public class BaseWeaponData : ScriptableObject
{
    public float fireRate = 10f;
    //public ParticleSystem hitParticlePrefab;
    //public ParticleSystem hitParticleEnemyPrefab;

    // Add other common weapon properties here
    public virtual void Shoot(Transform shootingPoint, Transform shootingPointRaycast)
    {
        // Implement shooting logic in derived classes
    }
}
