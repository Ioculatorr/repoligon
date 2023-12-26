using UnityEngine;

public class ChangeScriptableObject : MonoBehaviour
{
    public ShotgunSO scriptableObjectA;
    public ShotgunSO scriptableObjectB;

    private ShotgunSO currentScriptableObject;
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

    void SetScriptableObject(ShotgunSO newScriptableObject)
    {
        // Set the current ScriptableObject and spawn its associated prefab
        currentScriptableObject = newScriptableObject;

        // For demonstration purposes, you can print some information
        Debug.Log($"Switched to {currentScriptableObject.name}");

        // Spawn the associated prefab
        spawnedPrefab = Instantiate(currentScriptableObject.weaponPrefab, transform.position, Quaternion.identity);
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