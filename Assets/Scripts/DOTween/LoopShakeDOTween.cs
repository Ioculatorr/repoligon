using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopShakeDOTween : MonoBehaviour
{
    [SerializeField] private float shakeDur = 0.1f;
    [SerializeField] private float shakeStr = 0.1f;
    void Start()
    {
        this.gameObject.transform.DOShakePosition(shakeDur, shakeStr, 10, 15f)
            .SetLoops(-1, LoopType.Restart);
    }
}
