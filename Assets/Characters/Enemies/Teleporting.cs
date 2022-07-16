using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public GameObject[] TeleportZones;

    void Update()
    {
        TeleportCheck();
    }
    void TeleportCheck()
    {
        foreach (GameObject Zone in TeleportZones)
        {
            foreach (GameObject PlayerZone in TeleportZones)
            {
                if (Zone.GetComponent<EnemyZone>().EnemyIsInside==true && Zone.GetComponent<EnemyZone>().PlayerIsInside == false  && PlayerZone.GetComponent<EnemyZone>().EnemyIsInside == false && PlayerZone.GetComponent<EnemyZone>().PlayerIsInside == true)
                {
                    // Teleport(Zone, PlayerZone);
                    Zone.GetComponent<EnemyZone>().Enemy.GetComponent<Animator>().SetBool("BackToLife", false);
                    Zone.GetComponent<EnemyZone>().Enemy.GetComponent<Animator>().SetBool("Die",true);
                    StartCoroutine(Teleport(Zone, PlayerZone,1));
                }
            }
        }
    }
    IEnumerator Teleport(GameObject Zone, GameObject PlayerZone,float Delay)
    {
        yield return new WaitForSeconds(Delay);
        Zone.GetComponent<EnemyZone>().Enemy.transform.position = new Vector2 (PlayerZone.transform.position.x, PlayerZone.transform.position.y);
        Zone.GetComponent<EnemyZone>().Enemy.GetComponent<Animator>().SetBool("BackToLife", true);
        Zone.GetComponent<EnemyZone>().Enemy.GetComponent<Animator>().SetBool("Die", false);

        Zone.GetComponent<EnemyZone>().Enemy = null;
    }
}
