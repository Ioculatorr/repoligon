using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : BaseWeapon
{

    [SerializeField] private Light lampLight;
    
    public override void Shoot()
    {
        lampLight.intensity += 1;
    }
    void Update()
    {
        
    }

    public override void BulletEmit(Vector3 hitPoint)
    {
        
    }

    public override void SpawnHitParticleEnemy(Vector3 hitPoint)
    {
        
    }

    public override void SpawnHitParticle(Vector3 hitPoint)
    {
        
    }

    public override void PlayPrefabEffects()
    {
        
    }
}
