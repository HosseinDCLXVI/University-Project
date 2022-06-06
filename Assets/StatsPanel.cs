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

    public Text[] StatsArray=new Text[12];
    /*public Text Chapter1HighScore;
    public Text Chapter1Deaths;
    public Text Chapter1Kills;
    public Text Chapter1PlayTime;

    public Text Chapter2HighScore;
    public Text Chapter2Deaths;
    public Text Chapter2Kills;
    public Text Chapter2PlayTime;

    public Text Chapter3HighScore;
    public Text Chapter3Deaths;
    public Text Chapter3Kills;
    public Text Chapter3PlayTime;

    public Text Chapter4HighScore;
    public Text Chapter4Deaths;
    public Text Chapter4Kills;
    public Text Chapter4PlayTime;*/
    //HighScore - totalKills - time-totalDeaths


    void Start()
    {
        UserName.text = PlayerPrefs.GetString("UserName");
        
        Stats PlayerStats= StatSaver.LoadStats(PlayerPrefs.GetString("UserName"));
        for(int i=0;i<StatsArray.Length;i++)
        {
            StatsArray[i].text = PlayerStats.Level[i + 4].ToString();
        }

        TotalScore.text=PlayerStats.TotalScores.ToString(); 

        for (int i = 2; i < StatsArray.Length; i += 4)
        {
            TotalPlayTime.text = (int.Parse(TotalPlayTime.text) + int.Parse(StatsArray[i].text)).ToString();
        }

        for (int i = 3; i < StatsArray.Length; i += 4)
        {
            TotalDeaths.text = (int.Parse(TotalDeaths.text) + int.Parse(StatsArray[i].text)).ToString();
        }

        for (int i = 1; i < StatsArray.Length; i += 4)
        {
            TotalKills.text= (int.Parse(TotalKills.text)+int.Parse(StatsArray[i].text)).ToString();
        }
        //HighScore - totalKills - time-totalDeaths
        /*UserName.text = 

        TotalScore.text=
        TotalPlayTime.text=
        TotalKills.text=
        TotalDeaths.text=

        Chapter1HighScore.text=
        Chapter1Deaths.text=
        Chapter1Kills.text=
        Chapter1PlayTime.text=

        Chapter2HighScore.text=
        Chapter2Deaths.text =
        Chapter2Kills.text =
        Chapter2PlayTime.text =

        Chapter3HighScore.text =
        Chapter3Deaths.text=
        Chapter3Kills.text=
        Chapter3PlayTime.text=

        Chapter4HighScore.text=
        Chapter4Deaths.text=
        Chapter4Kills.text=
        Chapter4PlayTime.text=*/

    }

}
