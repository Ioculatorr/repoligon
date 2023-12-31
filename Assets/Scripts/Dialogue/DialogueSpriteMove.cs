using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DialogueSpriteMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpriteShake());
    }

    IEnumerator SpriteShake()
    {
        this.gameObject.transform.DOShakePosition(1f, 3f, 3, 3f);
        
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(SpriteShake());
    }
}
