using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenCombiner : MonoBehaviour
{
    void Start()
    {
        // Create your individual tweens
        Tween tween1 = transform.DOMoveX(5f, 2f);
        Tween tween2 = transform.DORotate(new Vector3(0f, 90f, 0f), 2f);
        Tween tween3 = transform.DOScale(new Vector3(2f, 2f, 2f), 2f);

        // Combine tweens using the Join method
        Sequence combinedSequence = DOTween.Sequence();
        combinedSequence.Append(tween1);
        combinedSequence.Join(tween2);
        combinedSequence.Join(tween3);

        // You can add more tweens using the Join method as needed

        // Play the combined sequence
        combinedSequence.Play();
    }
}