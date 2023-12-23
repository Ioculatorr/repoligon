using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FallEffects : MonoBehaviour
{
    public CharacterController characterController;
    public Volume postProcessVolume;

    [SerializeField] private float minFallingSpeed = 20f;
    [SerializeField] private float maxFallingSpeed = 200f;
    [SerializeField] private float minVignetteIntensity = 0f;
    [SerializeField] private float maxVignetteIntensity = 0.5f;

    private Vignette vignette;
    [SerializeField] private AudioSource fallAudio;
    [SerializeField] private GameObject fallCamera;

    private void Start()
    {
        if (postProcessVolume.profile.TryGet(out vignette))
        {
            // You can access other post-processing effects in a similar way
            // For example: postProcessVolume.profile.TryGet(out Bloom bloom);
        }
    }

    private void Update()
    {
        if (characterController.isGrounded)
        {
            // Reset the vignette when the character is grounded
            UpdateVignette(0f);
        }
        else if (characterController.velocity.y < -minFallingSpeed)
        {
            float normalizedSpeed = Mathf.InverseLerp(-minFallingSpeed, -maxFallingSpeed, characterController.velocity.y);
            float vignetteIntensity = Mathf.Lerp(minVignetteIntensity, maxVignetteIntensity, normalizedSpeed);

            UpdateVignette(vignetteIntensity);
        }
    }
    private void UpdateVignette(float intensity)
    {
        vignette.intensity.value = intensity;
        fallAudio.volume = intensity * 2;

            fallCamera.transform.DOShakeRotation(1f, intensity*100, 10, 15f, false)
        .SetLoops(-1, LoopType.Incremental)
        .SetEase(Ease.Linear);
    }
}
