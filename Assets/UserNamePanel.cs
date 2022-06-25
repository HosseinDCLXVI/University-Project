using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UserNamePanel : MonoBehaviour
{
    public Text UsernameTXT;
    void Awake()
    {
        if(PlayerPrefs.GetString("UserName") == "")
        {
            PlayerPrefs.SetString("UserName", "Guest");
            string path = Application.persistentDataPath + "/GuestStats.DCLXVI";
            UsernameTXT.text = PlayerPrefs.GetString("UserName");

            if (!File.Exists(path))
            {
                Stats NewStats = new Stats(null, null, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, 0, 0);
                LatestProgressSaver.SaveLatestProgress(0, 0, 0, 0, 0, 0, 0, 0, false);
                StatSaver.SaveStats(NewStats);
            }
        }


    }

    void Update()
    {
        UsernameTXT.text = PlayerPrefs.GetString("UserName");
        Cursor.visible = true;
    }
}
