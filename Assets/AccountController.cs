using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountController : MonoBehaviour
{
    public GameObject UserNamePanel;
    public Text UsernameTXT;

    public GameObject LogInPanel;
    public GameObject MainMenuPanel;

    void Awake()
    {
        if(PlayerPrefs.GetString("UserName") =="")
        {
            LogInPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
        }
        else
        {
            LogInPanel.SetActive(false);
            MainMenuPanel.SetActive(true);
        }
    }

    void Update()
    {
        UserNameShowCase();
    }
    void UserNameShowCase()
    {
        if (PlayerPrefs.GetString("UserName") == "")
        {
            
            UserNamePanel.SetActive(false);
        }
        else
        {
            UserNamePanel.SetActive(true);
            UsernameTXT.text = PlayerPrefs.GetString("UserName");
        }
    }

}
