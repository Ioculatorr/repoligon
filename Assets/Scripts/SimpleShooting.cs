using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Unity.VisualScripting;

public enum WeaponType
{
    Pistol = 0,
    Camera = 1,
    Lamp = 2
}
public class SimpleShooting : MonoBehaviour
{    
    
    [SerializeField] private Transform cameraShake;


    [Header("Weapons")]

    [SerializeField] private WeaponData scriptableObjectA;
    [SerializeField] private WeaponData scriptableObjectB;

    private WeaponData currentScriptableObject;
    [SerializeField] private BaseWeapon currentBaseWeapon;
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
        //SetScriptableObject(scriptableObjectA);
        
    }

    void Update()
    {
        // Check for user input (e.g., space key) to trigger shooting
        if (Input.GetKey(KeyCode.Mouse0) && !PickedUpSmth)
        {
            currentBaseWeapon.Shoot();
        }
        // Check for input or any condition to switch between ScriptableObjects
        if (Input.GetKeyDown(KeyCode.Q)&& !PickedUpSmth)
        {
            // Toggle between the two ScriptableObjects
            ToggleScriptableObject();
        }
        if (Input.GetKeyDown(KeyCode.R) && !PickedUpSmth && !AimAtYourself)
        {
            // Kill yourself
            LifeRestart();
        }
        else if (Input.GetKeyDown((KeyCode.R)) && !PickedUpSmth && AimAtYourself)
        {
            LifeChangeMind();
        }
    }
    
    void ToggleScriptableObject()
    {
        //Destroy(currentBaseWeapon.gameObject);

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
        //currentScriptableObject = newScriptableObject;

        //TODO ZMIEN WEAPONDATA NA ENUM / PREFAB 

    }
    

    private void LifeRestart()
    {
        AimAtYourself = true;
        this.AddComponent<AudioSource>().PlayOneShot(suicideSound);
        Destroy(this.GetComponent<AudioSource>(), suicideSound.length);
        //TODO
        spawnedPrefab.transform.DOLookAt(cameraShake.transform.position, 0.2f);
    }

    private void LifeChangeMind()
    {
        AimAtYourself = false;
        spawnedPrefab.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.2f);
    }

    public void HideWeaponOnPickUp()
    {
        PickedUpSmth = true;
        Destroy(currentBaseWeapon);
    }
    
    public void SpawnWeaponOnDropOff()
    {
        PickedUpSmth = false;
        SetScriptableObject(scriptableObjectA);
    }
}
