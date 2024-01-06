using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeMaterial : MonoBehaviour
{
    public FlickerLight isFlickering;

    public Material newBulbMaterial;
    public Material oldBulbMaterial;



    // Start is called before the first frame update
    void Start()
    {
        oldBulbMaterial = GetComponent<Renderer>().material;
    }

    private void ChangeMaterialNow()
    {
        if (isFlickering == true)
        {
            GetComponent<Renderer>().material = newBulbMaterial;
        }
        else
        GetComponent<Renderer>().material = oldBulbMaterial;
    }
}
