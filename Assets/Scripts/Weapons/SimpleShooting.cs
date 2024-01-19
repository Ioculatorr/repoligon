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
    
    [SerializeField] private BaseWeapon currentBaseWeapon;
    public BaseWeapon[] weaponDataArray;  // Array to hold different weapon data
    private int currentWeaponIndex = 0; // Index to track the current weapon
    
    private bool PickedUpSmth = false;
    private GameObject spawnedPrefab;
    private Tween gunShakeTween;

    [SerializeField] private UnityEvent onShoot;



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
        if (Input.GetKeyDown(KeyCode.Mouse1) && !PickedUpSmth)
        {
            currentBaseWeapon.AltShoot();
        }
        // Check for input or any condition to switch between ScriptableObjects
        if (Input.GetKeyDown(KeyCode.Q)&& !PickedUpSmth)
        {
            // Toggle between the two ScriptableObjects
            //ToggleScriptableObject();
        }
        if (Input.GetKeyDown(KeyCode.R) && !PickedUpSmth && !currentBaseWeapon.AimAtYourself)
        {
            // Kill yourself
            //LifeRestart();
            currentBaseWeapon.LifeRestart();
        }
        else if (Input.GetKeyDown((KeyCode.R)) && !PickedUpSmth && currentBaseWeapon.AimAtYourself)
        {
            //LifeChangeMind();
            currentBaseWeapon.LifeChangeMind();
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
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

    public void HideWeaponOnPickUp()
    {
        PickedUpSmth = true;
        currentBaseWeapon.DestroyModel();
    }
    
    public void SpawnWeaponOnDropOff()
    {
        PickedUpSmth = false;
        
        // Assign the new weapon data
        currentBaseWeapon = weaponDataArray[currentWeaponIndex];
            
        // Spawn the model of the new weapon
        currentBaseWeapon.SpawnModel();
    }
}
