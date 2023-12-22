using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopShakeDOTween : MonoBehaviour
{
    [SerializeField] private float shakeDur = 1f;
    [SerializeField] private float shakeStr = 3f;
    [SerializeField] private int shakeVib = 10;
    void Start()
    {
        this.gameObject.transform.DOShakeRotation(shakeDur, shakeStr, shakeVib, 90f, false)
            .SetLoops(-1, LoopType.Restart);
    }
}
