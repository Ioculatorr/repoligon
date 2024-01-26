using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenCombiner : MonoBehaviour
{
    [SerializeField] private float loopShakeDur = 1f;
    [SerializeField] private float loopShakeStr = 3f;
    [SerializeField] private int loopShakeVib = 10;
    
    public float bobbingAmount = 0.5f;
    public float bobbingSpeed = 0.5f;

    void Start()
    {

        // Create your individual tweens
        Tween loopTween = this.gameObject.transform.DOShakeRotation(loopShakeDur, loopShakeStr, loopShakeVib, 30f, false)
            .SetLoops(-1, LoopType.Incremental);
        
        //Tween tween2 = transform.DORotate(new Vector3(0f, 90f, 0f), 10f);
        //Tween tween3 = transform.DOScale(new Vector3(2f, 2f, 2f), 10f);

        // Combine tweens using the Join method
        Sequence combinedSequence = DOTween.Sequence();
        combinedSequence.Append(loopTween);
        //combinedSequence.Join(tween2);
        //combinedSequence.Join(tween3);

        // You can add more tweens using the Join method as needed
        
        // Add head bobbing to the combined sequence
        //combinedSequence.AppendCallback(StartHeadBobbing);

        // Play the combined sequence
        combinedSequence.Play();
    }
}