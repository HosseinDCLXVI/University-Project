using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UserNamePanel : MonoBehaviour
{
    public Text UsernameTXT;
    void Start()
    {
        if(PlayerPrefs.GetString("UserName") is null)
        {
            PlayerPrefs.SetString("UserName", "Guest");

            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/GuestStats.DCLXVI";
            if (!File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Create);
                Stats NewStats = new Stats(0,null,null,null,0,0);
                formatter.Serialize(stream, NewStats);
                stream.Close();
            }
        }


    }

    void Update()
    {
        UsernameTXT.text = PlayerPrefs.GetString("UserName");
    }
}
