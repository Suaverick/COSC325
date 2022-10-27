using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    
    public void GoToGameScene()
    {
        SceneManager.LoadScene("TestLevel");
    }

}
