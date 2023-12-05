using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        StartCoroutine(LookAfterSeconds());
    }

    IEnumerator LookAfterSeconds()
    {
        transform.DOLookAt(player.transform.position, 1f);

        this.transform.DOShakeRotation(0.2f, 15f, 2, 10f);

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(LookAfterSeconds());
    }
}
