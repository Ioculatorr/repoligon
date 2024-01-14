using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeAnimation : MonoBehaviour
{
    private Animator mAnimator;
    private bool inRange = false;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && inRange)
            {
                if (isOpen == false)
                {
                    mAnimator.SetTrigger("Open");
                    isOpen = true;
                }
                else if (isOpen == true)
                {
                    mAnimator.SetTrigger("Close");
                    isOpen = false;
                }
            }
        }
    }
}
