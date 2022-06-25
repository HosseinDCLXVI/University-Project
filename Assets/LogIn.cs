using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LogIn : MonoBehaviour
{
    public InputField UserName;
    public InputField Password;
    public GameObject WrongUserTXT;
    public GameObject WrongPasswordTXT;

    public void logInBTN()
    {     
        string Path = Application.persistentDataPath + "/" + UserName.text + "Stats.DCLXVI";
        if (File.Exists(Path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path, FileMode.Open);
            Stats PlayerStats = formatter.Deserialize(stream) as Stats;
            WrongUserTXT.SetActive(false);
            if (PlayerStats.PlayerPassword==Password.text)
            {
                PlayerPrefs.SetString("UserName", UserName.text);
                WrongPasswordTXT.SetActive(false);
            }
            else
            {
                WrongPasswordTXT.SetActive(true);
            }
            stream.Close();
        }
        else
        {
            WrongUserTXT.SetActive(true);
        }
    }
}
