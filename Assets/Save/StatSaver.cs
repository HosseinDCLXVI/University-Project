using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class StatSaver
{
    
    public static void SaveStats( Stats TempStats )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string UserName =PlayerPrefs.GetString("UserName");
        string Path = Application.persistentDataPath + "/" + UserName + "Stats.DCLXVI";
        FileStream stream = new FileStream(Path, FileMode.Create);
        formatter.Serialize(stream, TempStats);
        stream.Close();
    }
    public static Stats LoadStats(string UserName)
    {
        string Path = Application.persistentDataPath + "/"+ UserName +"Stats.DCLXVI";
        if(File.Exists(Path))
        {
            BinaryFormatter formatter=new BinaryFormatter();
            FileStream stream = new FileStream(Path, FileMode.Open);
            Stats PlayerStats = formatter.Deserialize(stream) as Stats;
            stream.Close();
            return PlayerStats;
        }
        else
        return null;
    }


}