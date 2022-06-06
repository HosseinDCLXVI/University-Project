using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform EnemyTransform;
    public Transform HealthCanvas;
    public Animator EnemyAnimator;
    public Vector2 EnemySpeed;
    public bool GoRight;// comes from EnemyZone Script
    public bool NearPlayer;
    public bool Visited =false;
    public GameObject EnemyItself;



    void FixedUpdate()
    {
        EnemyMovement();
        if(Visited)
        {
            EnemyAnimator.SetBool("BackToLife", true);
        }
    }



    void EnemyMovement()
    {
        if (!NearPlayer&&Visited)
        {
            EnemyAnimator.SetBool("Walk", true);
            EnemyTransform.Translate(EnemySpeed * Time.deltaTime);
            if (GoRight)
            {
                EnemyTransform.eulerAngles = new Vector3(0, 0, 0);
                HealthCanvas.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                EnemyTransform.eulerAngles = new Vector3(0, 180, 0);
                HealthCanvas.eulerAngles=new Vector3(0,0, 0);
            }
        }
        else
        {
            EnemyAnimator.SetBool("Walk", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//enemy stops movingg when near the player and attacks
    {
        if (collision.tag == "Player")
        {
            EnemyMeleeAttack enemy = EnemyItself.GetComponent<EnemyMeleeAttack>();
            if (enemy != null)
            EnemyItself.GetComponent<EnemyMeleeAttack>().NearPlayer = true;
            NearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//enemy starts movingg when far from the player
    {
        if (collision.tag == "Player")
        {
            EnemyMeleeAttack enemy = EnemyItself.GetComponent<EnemyMeleeAttack>();
            if (enemy != null)
             EnemyItself.GetComponent<EnemyMeleeAttack>().NearPlayer = false;
            NearPlayer = false;
        }
    }
}
        


    

