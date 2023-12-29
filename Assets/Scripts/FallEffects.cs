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
    //private PlayerMovement boolGrounded;
    //private bool canStartCoroutine = true;

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


        //if (boolGrounded.isGrounded == false && canStartCoroutine == true)
        //{
        //    StartCoroutine(CameraFallShake());
        //    canStartCoroutine = false;
        //}
        //else if(boolGrounded.isGrounded == true)
        //{
        //    StopCoroutine(CameraFallShake());
        //    canStartCoroutine = true;
        //}

        if (characterController.velocity.magnitude > minFallingSpeed)
        {
            float normalizedSpeed = Mathf.InverseLerp(minFallingSpeed, maxFallingSpeed, characterController.velocity.magnitude);
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
                fallCamera.transform.DOShakeRotation(1f, intensity * 50f, 10, 15f, false)
                                                    .SetLoops(-1, LoopType.Incremental)
                                                    .SetEase(Ease.Linear);

                yield return new WaitForSeconds(0.25f);
        }
    }
}
