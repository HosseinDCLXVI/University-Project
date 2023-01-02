using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpotScript : MonoBehaviour
{


     void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<ProgressManager>().IsCrouch)
            {
                collision.GetComponent<ProgressManager>().PlayerIsVisible = false;
            }
            else
            {
                collision.GetComponent<ProgressManager>().PlayerIsVisible = true;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<ProgressManager>().IsCrouch)
                collision.GetComponent<ProgressManager>().PlayerIsVisible = true;
        }                
    }

}
