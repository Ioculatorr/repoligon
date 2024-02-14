using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class Headbobbing : MonoBehaviour
{
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private float headbobFrequency = 5f;
    [SerializeField] private float headbobAmplitude = 0.1f;

    [SerializeField] private float weaponBobFrequency = 7f;
    [SerializeField] private float weaponBobAmplitude = 0.05f;

    private bool isMoving;

    public void SetIsMoving(bool moving)
    {
        isMoving = moving;

        if (!isMoving)
        {
            // Reset headbobbing and weapon bobbing when not moving
            cameraObj.transform.DOLocalMove(Vector3.zero, 0.1f).SetEase(Ease.Linear);
            weaponTransform.DOLocalMove(Vector3.zero, 0.1f).SetEase(Ease.Linear);
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            // Head bobbing
            float headbobX = Mathf.Sin(Time.time * headbobFrequency) * headbobAmplitude;
            float headbobY = Mathf.Cos(Time.time * 2f * headbobFrequency) * headbobAmplitude;
            Vector3 headbobPosition = new Vector3(headbobX, headbobY, 0f);
            cameraObj.transform.DOLocalMove(headbobPosition, 0.1f).SetEase(Ease.Linear);

            // Weapon bobbing
            float weaponBobX = Mathf.Sin(Time.time * weaponBobFrequency) * weaponBobAmplitude;
            float weaponBobY = Mathf.Cos(Time.time * 2f * weaponBobFrequency) * weaponBobAmplitude;
            Vector3 weaponBobPosition = new Vector3(weaponBobX, weaponBobY, 0f);
            weaponTransform.DOLocalMove(weaponBobPosition, 0.1f).SetEase(Ease.Linear);
        }
    }
}