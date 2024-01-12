using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : BaseWeapon
{

    [SerializeField] private Light lampLight;

    private bool isLightOn = false;
    private bool canChangeLight = true;
    
    public override void Shoot()
    {
        if (!isLightOn && canChangeLight)
        {
            lampLight.enabled = true;
            isLightOn = true;
            canChangeLight = false;

            PlayPrefabEffects();
            
            StartCoroutine(LampWaitTime());
        }
        else if (isLightOn && canChangeLight)
        {
            lampLight.enabled = false;
            isLightOn = false;
            canChangeLight = false;

            PlayPrefabEffects();
            
            StartCoroutine(LampWaitTime());
        }
    }

    IEnumerator LampWaitTime()
    {
        yield return new WaitForSeconds(0.2f);
        canChangeLight = true;
    }

    public override void PlayPrefabEffects()
    {
        weaponAudio.Play();
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
}
