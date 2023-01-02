using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StatsPanel : MonoBehaviour
{
    
    public Text UserName;

    public Text TotalScore;
    public Text TotalPlayTime;
    public Text TotalDeaths;
    public Text TotalKills;

    int SumScores=0;
    int SumPlayTime = 0;
    int SumDeaths = 0;
    int SumKills=0;

    public Text[] HighestScore = new Text[3];
    public Text[] Deaths = new Text[3];
    public Text[] Kills = new Text[3];
    public Text[] PlayTime = new Text[3];



    void Awake()
    {
        UserName.text = PlayerPrefs.GetString("UserName");
        
        Stats PlayerStats= StatSaver.LoadStats(PlayerPrefs.GetString("UserName"));


        //TotalScore.text=PlayerStats.TotalScores.ToString();
        for (int i = 0; i < HighestScore.Length; i++)
        {
            SumScores += PlayerStats.HighestScore[i];
            TotalScore.text = SumScores.ToString();
        }

        for (int i = 0; i < PlayTime.Length; i ++)
        {
            SumPlayTime += PlayerStats.PlayTime[i];
            TotalPlayTime.text = SumPlayTime.ToString();
        }

        for (int i = 0; i < Deaths.Length; i ++)
        {
            SumDeaths += PlayerStats.Deaths[i];
            TotalDeaths.text = SumDeaths.ToString();
        }

        for (int i = 0; i < Kills.Length; i ++)
        {
            SumKills += PlayerStats.Kills[i];
            TotalKills.text = SumKills.ToString();
        }


        for (int i = 0; i < HighestScore.Length; i++)
        {
            HighestScore[i].text = PlayerStats.HighestScore[i].ToString();
        }
        for (int i = 0; i < Deaths.Length; i++)
        {
            Deaths[i].text = PlayerStats.Deaths[i].ToString();
        }
        for (int i = 0; i < Kills.Length; i++)
        {
            Kills[i].text = PlayerStats.Kills[i].ToString();
        }
        for (int i = 0; i < PlayTime.Length; i++)
        {
            PlayTime[i].text = PlayerStats.PlayTime[i].ToString();
        }       
    }
    

}
