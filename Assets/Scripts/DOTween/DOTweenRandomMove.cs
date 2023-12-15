using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomUpAndDownMovement : MonoBehaviour
{
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float fallDuration = 4f;
    [SerializeField] private float minYPosition = 1f;
    [SerializeField] private float maxYPosition = 5f;

    void Start()
    {
        MoveUp();
    }

    void MoveUp()
    {
        // Randomly set the target position within the specified range
        float randomY = Random.Range(minYPosition, maxYPosition);

        // Move up to the random position
        transform.DOMoveY(randomY, moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => FallDown(randomY));
    }

    void FallDown(float previousY)
    {
        // Fall down slowly to the previous position
        transform.DOMoveY(previousY, fallDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(MoveUp);
    }
}