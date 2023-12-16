using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectDOTween : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float fadeStartDistance = 10f;
    [SerializeField] private float fadeEndDistance = 5f;
    [SerializeField] private float fadeFrequency = 0.1f;

    private Material objectMaterial;
    private float initialAlpha;
    private Coroutine fadeCoroutine;

    void Start()
    {
        // Assuming your object has a material
        objectMaterial = GetComponent<Renderer>().material;

        // Store the initial alpha value
        initialAlpha = objectMaterial.color.a;

        // Start the fading coroutine if the player is within range
        if (IsPlayerInRange())
        {
            fadeCoroutine = StartCoroutine(FadeMaterial());
        }
    }

    void Update()
    {
        // Check if the player is within range
        bool playerInRange = IsPlayerInRange();

        // Check if the coroutine should start or stop
        if (playerInRange && fadeCoroutine == null)
        {
            fadeCoroutine = StartCoroutine(FadeMaterial());
        }
        else if (!playerInRange && fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }
    }

    bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= fadeStartDistance;
    }

    IEnumerator FadeMaterial()
    {
        while (true)
        {
            // Calculate the distance between the object and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Map the distance to a range between fadeStartDistance and fadeEndDistance
            float mappedDistance = Mathf.Clamp(distanceToPlayer, fadeEndDistance, fadeStartDistance);

            // Calculate the alpha value based on the mapped distance
            float alpha = Mathf.InverseLerp(fadeEndDistance, fadeStartDistance, mappedDistance);

            // Smoothly fade the material alpha
            objectMaterial.color = new Color(objectMaterial.color.r, objectMaterial.color.g, objectMaterial.color.b, alpha);

            // Wait for the next frame
            yield return new WaitForSeconds(fadeFrequency);
        }
    }
}
