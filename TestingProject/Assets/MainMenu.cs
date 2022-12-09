using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    
    public void openSettings()
    {
        // Name of scene or scene index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
    
    public void selectLevel()
    {
        SceneManager.LoadScene("Level Select");
    }
    
    public void returnToMain()
    {
        SceneManager.LoadScene("MainMenu");

    }


}
