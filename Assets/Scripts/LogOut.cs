using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogOut : MonoBehaviour
{
    public Text BtnTxt;
    void Update()
    {
        if(PlayerPrefs.GetString("UserName")== "Guest")
        {
            BtnTxt.text = "LogIn";
        }
        else
        {
            BtnTxt.text = "LogOut";
        }
    }
    public void LogOutBTN()
    {
        PlayerPrefs.SetString("UserName", "");
    }
}
