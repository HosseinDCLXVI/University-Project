using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public GameObject[] TeleportZones;

    void Update()
    {
        TeleportCheck();
        //teleport();
    }
    void TeleportCheck()
    {
        foreach (GameObject Zone in TeleportZones)
        {
            foreach (GameObject PlayerZone in TeleportZones)
            {
                if (Zone.GetComponent<EnemyZone>().EnemyIn==true && Zone.GetComponent<EnemyZone>().PlayerIn == false  && PlayerZone.GetComponent<EnemyZone>().EnemyIn == false && PlayerZone.GetComponent<EnemyZone>().PlayerIn == true)
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
    /*void Teleport(GameObject Zone,GameObject PlayerZone)
    {
        Zone.GetComponent<EnemyZone>().Enemy.transform.position = PlayerZone.transform.position;
    }*/


}
