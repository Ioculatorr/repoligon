using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExit: MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            // Call the function to exit the game
            Exit();
        }
    }

    // Function to exit the game
    void Exit()
    {
        Application.Quit(); // Quit the application
    }
}
