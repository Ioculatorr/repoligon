using UnityEngine;
using DG.Tweening;

public class ElevatorDoors : MonoBehaviour
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease easeType = Ease.Linear;

    private Vector3 leftStartPosition;
    private Vector3 rightStartPosition;

    void Start()
    {
        // Store initial closed positions of the doors
        leftStartPosition = leftDoor.position;
        rightStartPosition = rightDoor.position;
    }

    public void OpenDoors()
    {
        // Slide left door to open position
        leftDoor.DOLocalMoveX(leftDoor.transform.localPosition.x + 2, duration).SetEase(easeType);

        // Slide right door to open position
        rightDoor.DOLocalMoveX(rightDoor.transform.localPosition.x - 2, duration).SetEase(easeType);
    }

    public void CloseDoors()
    {
        // Slide left door back to closed position
        leftDoor.DOLocalMove(leftStartPosition, duration).SetEase(easeType);

        // Slide right door back to closed position
        rightDoor.DOLocalMove(rightStartPosition, duration).SetEase(easeType);
    }
}

