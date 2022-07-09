using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
          LatestProgressSaver.SaveLatestProgress(transform.position.x, transform.position.y, collision.GetComponent<Transform>().position.z, collision.GetComponent<ProgressManager>().level, collision.GetComponent<ProgressManager>().time, collision.GetComponent<PlayerHealth>().CurrentHealth, collision.GetComponent<PlayerStamina>().CurrentStamina, collision.GetComponent<ProgressManager>().KillPoints,true);
        }
    }

}
