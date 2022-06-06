using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public GameObject CompletionPanel;
     GameObject Player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // collision.GetComponent<PlayerMovement>().CanMove = false;
            Player = collision.gameObject;
            collision.GetComponent<Animator>().SetBool("Run",false);
            collision.GetComponent<Animator>().SetBool("Jump", false); 
            collision.GetComponent<Animator>().SetBool("Falling", false);
            Destroy(collision.GetComponent<PlayerMovement>());
            Destroy(collision.GetComponent<PlayerAttack>());
            Invoke("ShowCompletionPanel", 5);
        }
    }

    void ShowCompletionPanel()
    {
        CompletionPanel.SetActive(true);
        Player.GetComponent<PlayerHealth>().BlurEffect.weight = 1f;
        Time.timeScale = 0;
    }
    
}
