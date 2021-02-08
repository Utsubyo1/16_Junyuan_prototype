using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameisPaused = false;

    public GameObject pausemenuUI;
    // Update is called once per frame
    void Update()
    {
        //when press 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                //Resume game
                Resume();
            }
            else
            {
                //Pause game
                Pause();
            }
        }
    }

    public void Resume()
    {
        //resume and set the time to default
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    void Pause()
    {
        // pause and set time to 0
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }
    
    public void LoadMenu()
    {
        //change scene to menu scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
        Debug.Log("Menu");
    }
    public void QuitGame()
    {
        //quit game
        Debug.Log("Quitting game. . .");
        Application.Quit();
    }
}

