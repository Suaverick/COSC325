using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class controls menu movement for the main menu
// This class on works on the play button for right now
public class GameMaster : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("TestLevel");
    }

}
