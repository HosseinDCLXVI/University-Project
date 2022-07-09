using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuESCBTN : MonoBehaviour
{
    public GameObject LastPanel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            LastPanel.SetActive(true);
        }
    }
}
