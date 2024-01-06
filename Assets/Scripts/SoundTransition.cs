using UnityEngine;
using DG.Tweening;

public class SoundTransition : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;

    // private bool useMethod1 = true;
    //
    // private void Update()
    // {
    //     // Check for the space bar press
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         // Toggle between methods
    //         useMethod1 = !useMethod1;
    //
    //         // Call the appropriate method based on the current state
    //         if (useMethod1)
    //         {
    //             Method1();
    //         }
    //         else
    //         {
    //             Method2();
    //         }
    //     }
    // }

    public void Method1()
    {
        audioSource1.DOFade(0f, 3f);
        audioSource2.DOFade(0.1f, 3f);
    }

    public void Method2()
    {
        audioSource2.DOFade(0f, 3f);
        audioSource1.DOFade(0.1f, 3f);
    }
}