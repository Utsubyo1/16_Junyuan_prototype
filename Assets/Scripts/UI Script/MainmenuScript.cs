using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Gamescene");
        PauseMenuScript.GameisPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("QUITTING . . .");
        Application.Quit();
    }
    public void LoadMenu()
    {
        //change scene to menu scene
        Time.timeScale = 1f;
        PauseMenuScript.GameisPaused = false;
        SceneManager.LoadScene("MenuScene");
        //Debug.Log("Menu");
    }
}
