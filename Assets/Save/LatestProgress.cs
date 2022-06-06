using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LatestProgress
{
    public float[] LastCheckpointPosition=new float[3];
    public float Level;
    public float Time;
    public float Health;
    public float Stamina;
    public float Points;
    public bool SavedInTheMiddle;
    public LatestProgress(float lastcheckpointpositionx, float lastcheckpointpositiony, float lastcheckpointpositionz, float level,float time, float health, float stamina , float points,bool savedinthemiddle)
    {
        LastCheckpointPosition[0] = lastcheckpointpositionx;
        LastCheckpointPosition[1] = lastcheckpointpositiony;
        LastCheckpointPosition[2] = lastcheckpointpositionz;
        Level = level;
        Time = time;
        Health = health;
        Stamina = stamina;
        Points = points;
        SavedInTheMiddle = savedinthemiddle;
    }
}
