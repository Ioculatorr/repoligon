using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    [SerializeField] private bool isFlickering = false;
    [SerializeField] private float timeDelay;

    //public Material newBulbMaterial;
    //public Material oldBulbMaterial;


    void Start()
    {
        //oldBulbMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }
    IEnumerator FlickeringLight()
    {
        isFlickering = true;

        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.2f, 1f);
        //GetComponent<Renderer>().material = newBulbMaterial;
        yield return new WaitForSeconds(timeDelay);

        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.2f, 1f);
        //GetComponent<Renderer>().material = oldBulbMaterial;
        yield return new WaitForSeconds(timeDelay);

        isFlickering = false;
    }
}
