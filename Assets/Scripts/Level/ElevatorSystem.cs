using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ElevatorSystem : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private GameObject platform;

    [SerializeField] private AudioClip elevatorBell;
    
    public bool isElevating = false;
    
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease easeType = Ease.InOutQuad;

    private Vector3 leftStartPosition;
    private Vector3 rightStartPosition;

    [SerializeField] private UnityEvent buttonTurnOff;
    
    
    void Start()
    {
        // Store initial closed positions of the doors
        leftStartPosition = leftDoor.position;
        rightStartPosition = rightDoor.position;
    }

    private void ElevatorComplete()
    {
        isElevating = false;
        this.GetComponent<AudioSource>().PlayOneShot(elevatorBell);

        OpenDoors();
        buttonTurnOff.Invoke();
    }

    public void Floor1()
    {
        if (isElevating == false)
        {
            CloseDoors();
            isElevating = true;

            platform.transform.DOMoveY(0f, 10f)
                .SetSpeedBased()
                .SetDelay(1)
                .OnComplete(ElevatorComplete);
        }
    }
    
    public void Floor0()
    {
        if (isElevating == false)
        {
            CloseDoors();
            isElevating = true;
            
            
            platform.transform.DOMoveY(-59f, 10f)
                .SetSpeedBased()
                .SetDelay(1)
                .OnComplete(ElevatorComplete);
        }
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
