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
    bool Paused = false;
    public Volume GlobalVolume;

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
            }
            else if (Paused)
            {
                PauseMenu.SetActive(false);
                PauseSettings.SetActive(false);
                GlobalVolume.weight = 0f;
                Paused = false;
                Time.timeScale = 1f;
            }
        }
    }
    public void ResumeBTN()
    {
        PauseMenu.SetActive(false);
        GlobalVolume.weight = 0f;
        Paused = false;
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
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
