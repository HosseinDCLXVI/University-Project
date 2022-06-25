using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SignUp : MonoBehaviour
{
    public GameObject SignUpUsernameError;
    public InputField Username;
    public InputField Password;
    public InputField Email;

    public void SignUpBTN()
    {
        BinaryFormatter formatter=new BinaryFormatter();
        string path = Application.persistentDataPath + "/"+Username.text+"Stats.DCLXVI";
        if (File.Exists(path))
        {
            SignUpUsernameError.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("UserName", Username.text);
            SignUpUsernameError.SetActive(false);
            Stats NewStats = new Stats(Password.text, Email.text, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, 0, 0);
            StatSaver.SaveStats(NewStats);
            LatestProgressSaver.SaveLatestProgress(0, 0, 0, 0, 0, 0, 0, 0, false);


        }

    }
}
