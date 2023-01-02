using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public string PlayerUsername;
    public string PlayerPassword;
    public string EmailAddress;
    public int[] HighestScore = new int[3];
    public int[] Deaths = new int[3];
    public int[] Kills = new int[3];
    public int[] PlayTime = new int[3];
    public int TotalScores;
    public int CompletedLevels;




    public Stats(string Password, string Email, int[] highestscore,int[] deaths , int[] kills, int[] playtime, int totalscore, int completedlevels)
    {
        PlayerPassword = Password;
        EmailAddress = Email;
        TotalScores = totalscore;
        CompletedLevels = completedlevels;

        HighestScore = highestscore;
        Deaths=deaths;
        Kills = kills;
        PlayTime = playtime;
    }
}
