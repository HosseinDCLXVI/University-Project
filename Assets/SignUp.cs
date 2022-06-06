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
            SignUpUsernameError.SetActive(false);
            FileStream stream = new FileStream(path,FileMode.Create);
            Stats NewStats = new Stats(0,Password.text,Email.text,null,0,0);
            formatter.Serialize(stream, NewStats);
            stream.Close();
            PlayerPrefs.SetString("UserName", Username.text);
            LatestProgressSaver.SaveLatestProgress(0, 0, 0, 0, 0, 0, 0, 0, false);
        }

    }
}
