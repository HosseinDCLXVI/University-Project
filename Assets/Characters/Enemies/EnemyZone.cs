using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{

    public bool EnemyIn;
    public bool PlayerIn;
    public bool EnemyGoRight=true;
    public float PlayerPosition;
    public float EnemyPosition;
    bool Visited;
    [HideInInspector] public GameObject Enemy; //for teleporting script
    void Start()
    {
    }

    void FixedUpdate()
    {
        PlayerFollower();
        Enemy.GetComponent<EnemyPatrol>().Visited = Visited;
    }
    void PlayerFollower()
    {
        if (PlayerIn && EnemyIn)
        {
            if (PlayerPosition - EnemyPosition > 0)
            {
                EnemyGoRight = true;
            }
            else if (PlayerPosition - EnemyPosition < 0)
            {
                EnemyGoRight = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy = null;
            collision.GetComponent<CircleCollider2D>().enabled = false;
            EnemyIn = false;
            EnemyGoRight = !EnemyGoRight;
            collision.GetComponent<EnemyPatrol>().GoRight = EnemyGoRight;
        }
        if (collision.tag == "Player")
        {
            PlayerIn = false;
        }
    }
     void OnTriggerStay2D(Collider2D collision)
    {
         if(collision.tag=="Player")
        {
            PlayerPosition=collision.GetComponent<Transform>().position.x;
        }
        if (collision.tag=="Enemy")
        {
            Enemy = collision.gameObject;
            EnemyPosition = collision.GetComponent<Transform>().position.x;
            collision.GetComponent<EnemyPatrol>().GoRight = EnemyGoRight;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy = collision.gameObject;
            collision.GetComponent<CircleCollider2D>().enabled = true;
            EnemyIn = true;
        }
        if (collision.tag == "Player")
        {
            PlayerIn = true;
            Visited = true;
        }
    }
    
    
}
