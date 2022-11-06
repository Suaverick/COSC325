using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool boolIsPaused = false;

    public GameObject pauseMenuUI;

    // Calls the Pause Button at the top right of the screen
    public void PauseButton()
    {
        Pause();
    }

    // Calls the Resume Button in the pause menu
    public void ResumeButton()
    {
        Resume();
    }

    // Calls the settings button in the pause menu
    public void SettingsButton()
    {

    }

    // Calls the quit button in the pause menu
    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    // Resumes the game
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        boolIsPaused = false;
    }

    // Pauses the game, and brings up the pause menu
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        boolIsPaused = true;
    }

}
