using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syzyf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMove(new Vector3(0f, 0f, 10f), 10f)
    .SetLoops(-1, LoopType.Incremental)
    .SetEase(Ease.Linear);
    }
}
