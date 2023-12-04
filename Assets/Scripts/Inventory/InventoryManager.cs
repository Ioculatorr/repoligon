using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryMenu;
    private bool menuActivated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && menuActivated)
        {
            Time.timeScale = 1.0f;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !menuActivated)
        {
            Time.timeScale = 0f;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }
}
