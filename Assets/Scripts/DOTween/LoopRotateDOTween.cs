using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopRotateDOTween : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}
