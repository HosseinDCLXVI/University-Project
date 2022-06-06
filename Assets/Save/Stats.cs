using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public string PlayerUsername;
    public string PlayerPassword;
    public string EmailAddress;
    public int[] Level=new int[16];
    public int TotalScores;
    public int CompletedLevels;




    public Stats(int Chapter, string Password, string Email, int[] level, int totalscore, int completedlevel )
    {
      PlayerPassword=Password; 
      EmailAddress=Email;
      Level=level;
      TotalScores=totalscore;
      CompletedLevels=completedlevel;
    }
}
