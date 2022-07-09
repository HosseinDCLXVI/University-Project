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
    public GameObject MainMenuPanel;

    public void logInBTN()
    {     
        string Path = Application.persistentDataPath + "/" + UserName.text + "Stats.DCLXVI";
        if (File.Exists(Path))
        {
            /*
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path, FileMode.Open);
            Stats PlayerStats = formatter.Deserialize(stream) as Stats;
            */

            Stats PlayerStats = StatSaver.LoadStats(Path);
            WrongUserTXT.SetActive(false);
            if (PlayerStats.PlayerPassword==Password.text)
            {
                WrongPasswordTXT.SetActive(false);
                PlayerPrefs.SetString("UserName", UserName.text);
                MainMenuPanel.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else
            {
                WrongPasswordTXT.SetActive(true);
            }
        }
        else
        {
            WrongUserTXT.SetActive(true);
        }
    }
    public void  GuestFunc()
    {
        PlayerPrefs.SetString("UserName", "Guest");
        string path = Application.persistentDataPath + "/GuestStats.DCLXVI";
        if (!File.Exists(path))
        {
            Stats NewStats = new Stats(null, null, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, 0, 0);
            LatestProgressSaver.SaveLatestProgress(0, 0, 0, 0, 0, 0, 0, 0, false);
            StatSaver.SaveStats(NewStats);
        }
    }
}
