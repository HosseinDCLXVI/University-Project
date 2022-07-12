using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    #region Inspector Variables
   [HideInInspector] public int Totalkills = 0;
   [HideInInspector] public float KillPoints; //comes from PlayerAttack
   [HideInInspector] public float time;
   [HideInInspector] public float Health; //Comes from PlayerHealth
   [HideInInspector] public int TotalScore;
   [HideInInspector] public Text TimeShowCase;
   [HideInInspector] public Text PointShowCase;
   [HideInInspector] public int level;
    #endregion

    #region setting up the latest progress in the level
    private void Awake()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;

        if (Continue.DoContinue)// Do continue will be true when player clicks on the continue button in the main menu ---- it will be false if player selects new game or certain level
        {
            LoadLastProgress();
        }
        else
        {
            ResetLastProgress();
        }
    }

    private void LoadLastProgress()
    {
        LatestProgress latestProgress = LatestProgressSaver.LoadLatestProgress(PlayerPrefs.GetString("UserName"));
        if (latestProgress != null)
        {
            if (latestProgress.SavedInTheMiddle) //saved in the middle will be true when player starts a level and gets to a checkpoint and doesnt finish the level
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
    private void ResetLastProgress()
    {
        LatestProgress latestProgress = LatestProgressSaver.LoadLatestProgress(PlayerPrefs.GetString("UserName"));
        if (latestProgress != null)
        {
            latestProgress.SavedInTheMiddle = false;
            LatestProgressSaver.SaveLatestProgress(latestProgress.LastCheckpointPosition[0], latestProgress.LastCheckpointPosition[1], latestProgress.LastCheckpointPosition[2], latestProgress.Level, latestProgress.Time, latestProgress.Health, latestProgress.Stamina, latestProgress.Points, latestProgress.SavedInTheMiddle);
        }
    }

    #endregion

    #region Game Management
    void Update()
    {
            PointShowCase.text = KillPoints.ToString() + " Points";

            Timer();
            TotalScoreCalculator();
            InGameCursorManager();
    }
    void Timer()
    {
            time += Time.deltaTime;
            int IntTime = (int)time;
            TimeShowCase.text = IntTime.ToString() +" S";
    }
    void TotalScoreCalculator()
    {
        Health = GetComponent<PlayerHealth>().CurrentHealth;
        TotalScore = (int)(KillPoints * (Health + 10));
    }
    void InGameCursorManager()
    {
        if (Time.timeScale == 1)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
    #endregion
}
