using UnityEngine;

public class ChangeScriptableObject : MonoBehaviour
{
    public WeaponData scriptableObjectA;
    public WeaponData scriptableObjectB;

    private WeaponData currentScriptableObject;
    private GameObject spawnedPrefab;

    void Start()
    {
        // Initialize with the first ScriptableObject
        SetScriptableObject(scriptableObjectA);
    }

    void Update()
    {
        // Check for input or any condition to switch between ScriptableObjects
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Toggle between the two ScriptableObjects
            ToggleScriptableObject();
        }
    }

    void ToggleScriptableObject()
    {
        // Destroy the current prefab if it exists
        DestroySpawnedPrefab();

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
        currentScriptableObject = newScriptableObject;

        // For demonstration purposes, you can print some information
        Debug.Log($"Switched to {currentScriptableObject.name}");

        // Spawn the associated prefab
        spawnedPrefab = Instantiate(currentScriptableObject.weaponPrefab, transform.position, Quaternion.identity);

        spawnedPrefab.transform.parent = transform;
        spawnedPrefab.layer = 12;
    }

    void DestroySpawnedPrefab()
    {
        // Destroy the previously spawned prefab if it exists
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
        }
    }
}
