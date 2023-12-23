using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FallEffects : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Volume postProcessVolume;

    [SerializeField] private float minFallingSpeed = 20f;
    [SerializeField] private float maxFallingSpeed = 200f;
    [SerializeField] private float minVignetteIntensity = 0f;
    [SerializeField] private float maxVignetteIntensity = 0.5f;

    private Vignette vignette;
    [SerializeField] private AudioSource fallAudio;
    [SerializeField] private GameObject fallCamera;

    private float intensity;

    private void Start()
    {
        StartCoroutine(CameraFallShake());
        if (postProcessVolume.profile.TryGet(out vignette))
        {
            // You can access other post-processing effects in a similar way
            // For example: postProcessVolume.profile.TryGet(out Bloom bloom);
        }
    }

    private void Update()
    {
        if (characterController.velocity.y < -minFallingSpeed)
        {
            float normalizedSpeed = Mathf.InverseLerp(-minFallingSpeed, -maxFallingSpeed, characterController.velocity.y);
            intensity = Mathf.Lerp(minVignetteIntensity, maxVignetteIntensity, normalizedSpeed);

        }
        else
        {
            // Player is not falling, set intensity to zero
            intensity = 0f;
        }
            UpdateVignette();
    }

    private void UpdateVignette()
    {
        vignette.intensity.value = intensity;
        fallAudio.volume = intensity * 2;
    }

    IEnumerator CameraFallShake()
    {
        while(true)
        {
                fallCamera.transform.DOShakeRotation(1f, intensity * 100f, 10, 15f, false)
                                                    .SetLoops(-1, LoopType.Incremental)
                                                    .SetEase(Ease.Linear);

                Debug.Log("I am shaken");
                yield return new WaitForSeconds(0.1f);
        }
    }
}
