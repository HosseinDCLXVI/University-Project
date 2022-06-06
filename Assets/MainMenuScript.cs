using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    public GameObject ContinueButton;
    void Awake()
    {
        LatestProgress latestProgress = LatestProgressSaver.LoadLatestProgress(PlayerPrefs.GetString("UserName"));
        if (latestProgress.SavedInTheMiddle||latestProgress.Level>=2)
        {
            ContinueButton.SetActive(true);
        }
        else
        {
            ContinueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueBTN()
    {
        SceneManager.LoadScene((int)LatestProgressSaver.LoadLatestProgress(PlayerPrefs.GetString("UserName")).Level);
        Continue.DoContinue = true;
    }
}
