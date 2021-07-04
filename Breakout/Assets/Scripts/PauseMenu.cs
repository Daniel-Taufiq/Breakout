using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {

        if(!GameManager.Instance.state.Equals(GameManager.State.MENU))
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                FindObjectOfType<AudioManager>().Play("Menu_sound");
                if(isPaused == true)
                {
                    Resume();
                } else 
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Menu_sound");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        FindObjectOfType<AudioManager>().Play("Menu_sound");
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameManager.Instance.QuitGame();
    }
}
