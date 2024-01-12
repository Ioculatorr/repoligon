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
    
    public BaseWeapon[] weaponDataArray;  // Array to hold different weapon data
    private int currentWeaponIndex = 0;      // Index to track the current weapon
    private BaseWeapon currentWeaponData; // Reference to the current weapon dat


    void Start()
    {
        currentBaseWeapon.SpawnModel();
        
        if (weaponDataArray.Length > 0)
        {
            // Set the initial weapon data
            SwitchWeapon(0);
        }
        else
        {
            Debug.LogError("No weapon data assigned!");
        }
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
            //ToggleScriptableObject();
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
        ToggleScriptableObject();
    }
    
    void ToggleScriptableObject()
    {
        // Example: Switching between weapons (you can customize the logic)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
        // Add more conditions for switching to other weapons
    }

    private void SwitchWeapon(int newIndex)
    {
        if (newIndex >= 0 && newIndex < weaponDataArray.Length)
        {
            // Destroy the model of the current weapon
            currentBaseWeapon.DestroyModel();
            
            // Update the current weapon index
            currentWeaponIndex = newIndex;

            // Assign the new weapon data
            currentBaseWeapon = weaponDataArray[currentWeaponIndex];
            
            // Spawn the model of the new weapon
            currentBaseWeapon.SpawnModel();

            Debug.Log("Switched to weapon: " + currentBaseWeapon);
        }
        else
        {
            Debug.LogError("Invalid weapon index: " + newIndex);
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
