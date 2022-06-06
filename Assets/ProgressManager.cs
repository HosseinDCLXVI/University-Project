using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public int Totalkills=0;
    public float KillPoints; //comes from PlayerAttack
    public float time=0;
    public float Health; //Comes from PlayerHealth
    public int TotalScore;
    public Text TimeShowCase;
    public Text PointShowCase;
    public int level;

    private void Awake()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        if (Continue.DoContinue)
        {
            LatestProgress latestProgress = LatestProgressSaver.LoadLatestProgress(PlayerPrefs.GetString("UserName"));
            if (latestProgress != null)
            { 
                if(latestProgress.SavedInTheMiddle)
                {
                   transform.position = new Vector3(latestProgress.LastCheckpointPosition[0], latestProgress.LastCheckpointPosition[1], latestProgress.LastCheckpointPosition[2]);
                    GetComponent<ProgressManager>().level = (int)latestProgress.Level;
                    GetComponent<ProgressManager>().time = (int)latestProgress.Time;
                    GetComponent<PlayerHealth>().CurrentHealth = (int)latestProgress.Health;
                    GetComponent<PlayerStamina>().CurrentStamina = (int)latestProgress.Stamina;
                    GetComponent<ProgressManager>().KillPoints = (int)latestProgress.Points;
                }
            }
         Continue.DoContinue = false;
        }
        else
        {
            LatestProgress latestProgress = LatestProgressSaver.LoadLatestProgress(PlayerPrefs.GetString("UserName"));
            latestProgress.SavedInTheMiddle = false;
            LatestProgressSaver.SaveLatestProgress(latestProgress.LastCheckpointPosition[0], latestProgress.LastCheckpointPosition[1], latestProgress.LastCheckpointPosition[2], latestProgress.Level, latestProgress.Time, latestProgress.Health, latestProgress.Stamina, latestProgress.Points, latestProgress.SavedInTheMiddle);
        }
    }
    private void Update()
    {
        Health = GetComponent<PlayerHealth>().CurrentHealth;
        Timer();
        PointShowCase.text=KillPoints.ToString()+" Points";
        TotalScoreCalculator();
    }
    void Timer()
    {
        time += Time.deltaTime;
        int IntTime = (int)time;
        TimeShowCase.text=IntTime.ToString()+" S";
    }
    void TotalScoreCalculator()
    {
        TotalScore = (int)(KillPoints * Health / time);
    }
}
