using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public string level;
    void Start()
    {
        
    }

    // Update is called once per frame
  public void OpenScene()
    {
        SceneManager.LoadScene(level);
    }
  public void returnMain()
  {
      SceneManager.LoadScene("MainMenu");

  }
}
