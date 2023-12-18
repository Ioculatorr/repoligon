using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Headbobbing : MonoBehaviour
{
    [SerializeField] private Camera cameraObj;
    [SerializeField] private float headbobFrequency = 5f;
    [SerializeField] private float headbobAmplitude = 0.1f;

    private bool isMoving;
    public void SetIsMoving(bool moving)
    {
        isMoving = moving;

        if (!isMoving)
        {
            // Reset headbobbing when not moving
            cameraObj.transform.DOLocalMove(Vector3.zero, 0.1f).SetEase(Ease.Linear);
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            float headbobX = Mathf.Sin(Time.time * headbobFrequency) * headbobAmplitude;
            float headbobY = Mathf.Cos(Time.time * 2f * headbobFrequency) * headbobAmplitude;

            Vector3 headbobPosition = new Vector3(headbobX, headbobY, 0f);

            // Use DOTween to smoothly move the controller's position
            cameraObj.transform.DOLocalMove(headbobPosition, 0.1f).SetEase(Ease.Linear);
        }
    }
}
