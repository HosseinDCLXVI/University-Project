using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text Score;//
    public Text HighestScore;//
    public Text TotalDeaths;//
    public Text Kills;
    public Text TotalPlaytime;
    public GameObject Player;



    private void Awake()
    {
        SyncDetails();
        Cursor.visible = true;
    }
    void SyncDetails()
    {
        Stats TempStats = StatSaver.LoadStats(PlayerPrefs.GetString("UserName"));

        Kills.text = Player.GetComponent<ProgressManager>().Totalkills.ToString();
        TotalPlaytime.text = ((int)Player.GetComponent<ProgressManager>().time).ToString();
        Score.text = Player.GetComponent<ProgressManager>().TotalScore.ToString();

        if (TempStats.HighestScore[Player.GetComponent<ProgressManager>().level - 1] < int.Parse(Score.text))
        {
            TempStats.HighestScore[Player.GetComponent<ProgressManager>().level - 1] = int.Parse(Score.text);
        }

        HighestScore.text = TempStats.HighestScore[Player.GetComponent<ProgressManager>().level - 1].ToString();

        (TempStats.Deaths[Player.GetComponent<ProgressManager>().level-1]) += 1;
        TotalDeaths.text = (TempStats.Deaths[Player.GetComponent<ProgressManager>().level - 1]).ToString();

        TempStats.Kills[Player.GetComponent<ProgressManager>().level - 1]= int.Parse(Kills.text);
        TempStats.PlayTime[Player.GetComponent<ProgressManager>().level - 1] = int.Parse(TotalPlaytime.text);


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
