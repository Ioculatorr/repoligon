using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public GameObject flashlight;

    public AudioSource turnOn;
    public AudioSource turnOff;

    private bool on;
    private bool off;
    // Start is called before the first frame update
    void Start()
    {
        off = true;
        flashlight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(off && Input.GetKeyDown(KeyCode.F))
        {
            flashlight.SetActive(false);
            turnOff.Play();
            off = false;
            on = true;
        }
        else if (on && Input.GetKeyDown(KeyCode.F))
        {
            flashlight.SetActive(true);
            turnOff.Play();
            off = true;
            on = false;
        }
    }
}