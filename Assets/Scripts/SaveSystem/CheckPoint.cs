using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
          ProgressManager ProgressManagerScript = collision.GetComponent<ProgressManager>();
          LatestProgressSaver.SaveLatestProgress(transform.position.x, transform.position.y, collision.GetComponent<Transform>().position.z, ProgressManagerScript.level, ProgressManagerScript.time, ProgressManagerScript.CurrentHealth, ProgressManagerScript.CurrentStamina, ProgressManagerScript.KillPoints,true);
        }
    }

}
