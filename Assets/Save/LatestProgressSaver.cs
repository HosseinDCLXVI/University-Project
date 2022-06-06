using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LatestProgressSaver
{
    public static void SaveLatestProgress(float lastcheckpointpositionx, float lastcheckpointpositiony, float lastcheckpointpositionz, float level, float time, float health, float stamina, float points, bool savedinthemiddle)
    {
        string UserName = PlayerPrefs.GetString("UserName");
        string path = Application.persistentDataPath + "/" + UserName + "LatestProgress.DCLXVI";
        BinaryFormatter formatter = new BinaryFormatter();
        LatestProgress latestProgress = new LatestProgress(lastcheckpointpositionx, lastcheckpointpositiony, lastcheckpointpositionz, level, time, health, stamina, points,savedinthemiddle);
        FileStream Stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(Stream, latestProgress);
        Stream.Close();
    }
    public static LatestProgress LoadLatestProgress(string UserName)   
    {
        string path = Application.persistentDataPath + "/" + UserName + "LatestProgress.DCLXVI";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            LatestProgress latestProgress = formatter.Deserialize(stream) as LatestProgress;
            stream.Close();
            return latestProgress ;
        }
        return null;
    }
}
