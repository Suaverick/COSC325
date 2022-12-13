using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private int intRandom;

    public void PlayGame()
    {
        intRandom = Random.Range(0, 3);
        if (intRandom == 0) SceneManager.LoadScene("FinalLevel1");
        else if (intRandom == 1) SceneManager.LoadScene("FinalLevel2");
        else SceneManager.LoadScene("BossLevel");
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
