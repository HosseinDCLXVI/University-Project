using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
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

        Kills.text = Player.GetComponent<ProgressManager>().Totalkills.ToString();
        Playtime.text = ((int)Player.GetComponent<ProgressManager>().time).ToString();
        Score.text = Player.GetComponent<ProgressManager>().TotalScore.ToString();
        if (TempStats.Level[Player.GetComponent<ProgressManager>().level * 4] < int.Parse(Score.text))
        {
            TempStats.Level[Player.GetComponent<ProgressManager>().level * 4] = int.Parse(Score.text);
        }
        HighestScore.text= TempStats.Level[Player.GetComponent<ProgressManager>().level * 4].ToString();
        (TempStats.Level[Player.GetComponent<ProgressManager>().level * 4 + 3]) += 1;
        Deaths.text=(TempStats.Level[Player.GetComponent<ProgressManager>().level * 4 + 3]).ToString();

        StatSaver.SaveStats(TempStats);
    }

    public void TryAgain()
    {
        Continue.DoContinue = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void restart()
    {
        
    }

}
