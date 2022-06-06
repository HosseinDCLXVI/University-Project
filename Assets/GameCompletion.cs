using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameCompletion : MonoBehaviour
{
    public Text Score;
    public Text HighestScore;
    public Text Deaths;
    public Text Kills;
    public Text Playtime;
    public GameObject Player;


    private void Awake()
    {
        SyncDetails();
    }

    void SyncDetails()
    {
        Stats TempStats = StatSaver.LoadStats(PlayerPrefs.GetString("UserName"));
        LatestProgress latestProgress = LatestProgressSaver.LoadLatestProgress(PlayerPrefs.GetString("UserName"));

        Kills.text = Player.GetComponent<ProgressManager>().Totalkills.ToString();
        Playtime.text = ((int)Player.GetComponent<ProgressManager>().time).ToString();
        Score.text = Player.GetComponent<ProgressManager>().TotalScore.ToString();
        if (TempStats.Level[Player.GetComponent<ProgressManager>().level * 4] < int.Parse(Score.text))
        {
            TempStats.Level[Player.GetComponent<ProgressManager>().level * 4] = int.Parse(Score.text);
        }
        HighestScore.text = TempStats.Level[Player.GetComponent<ProgressManager>().level * 4].ToString();
        Deaths.text = (TempStats.Level[Player.GetComponent<ProgressManager>().level * 4 + 3]).ToString();
        if(TempStats.CompletedLevels<Player.GetComponent<ProgressManager>().level)
        {
            TempStats.CompletedLevels = Player.GetComponent<ProgressManager>().level;
        }
        latestProgress.Level = Player.GetComponent<ProgressManager>().level + 1;
        StatSaver.SaveStats(TempStats);
        LatestProgressSaver.SaveLatestProgress( latestProgress.LastCheckpointPosition[0], latestProgress.LastCheckpointPosition[1], latestProgress.LastCheckpointPosition[2], latestProgress.Level, latestProgress.Time, latestProgress.Health, latestProgress.Stamina, latestProgress.Points,false);

    }
}
