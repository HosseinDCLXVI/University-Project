using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PuseScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseSettings;
    bool Paused;
    public Volume GlobalVolume;

     void Start()
    {
        Paused = false;
        GlobalVolume.weight = 0f;
    }
    private void Update()
    {
        PauseEsc();
    }
    public void PauseEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Paused)
            {
                PauseMenu.SetActive(true);
                GlobalVolume.weight = 1f;
                Paused = true;
                Time.timeScale = 0f;
                Cursor.visible = true;
            }
            else if (Paused)
            {
                PauseMenu.SetActive(false);
                PauseSettings.SetActive(false);
                GlobalVolume.weight = 0f;
                Paused = false;
                Time.timeScale = 1f;
                Cursor.visible = false;
            }
        }
    }
    public void ResumeBTN()
    {
        PauseMenu.SetActive(false);
        GlobalVolume.weight = 0f;
        Paused = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
