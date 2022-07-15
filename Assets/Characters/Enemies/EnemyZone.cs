using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    #region Variables
    [HideInInspector] public GameObject Enemy; //for teleporting script
    [HideInInspector] public bool EnemyIsInside;
    [HideInInspector] public bool PlayerIsInside;
    private float PlayerPosition;
    private float EnemyPosition;
    private bool EnemyIsVisited;

    Collider2D ZoneColider;
    #endregion

    private void Update()
    {
        ZoneColider = GetComponent<Collider2D>();
        Enemy.GetComponent<EnemyController>().ZonesRightBorder = ZoneColider.transform.position.x + ZoneColider.bounds.size.x / 2;
        Enemy.GetComponent<EnemyController>().ZonesLeftBorder = ZoneColider.transform.position.x - ZoneColider.bounds.size.x / 2;
    }

    #region Main
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyIsInside = false;
            Enemy.GetComponent<EnemyController>().EnemyIsInsideTheZone = EnemyIsInside;
            collision.GetComponent<CircleCollider2D>().enabled = true;
        }
        if (collision.tag == "Player")
        {
            PlayerIsInside = false;
            Enemy.GetComponent<EnemyController>().PlayerIsInsideTheZone = PlayerIsInside;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
         if(collision.tag=="Player")
        {
            PlayerPosition=collision.GetComponent<Transform>().position.x;
            Enemy.GetComponent<EnemyController>().PlayerPositionInTheZone = PlayerPosition;
        }
        if (collision.tag=="Enemy")
        {
            if (collision != null)
                Enemy = collision.gameObject;

            EnemyPosition = collision.GetComponent<Transform>().position.x;
            Enemy.GetComponent<EnemyController>().EnemyPositionInTheZone = EnemyPosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision != null)
                Enemy = collision.gameObject;
            collision.GetComponent<CircleCollider2D>().enabled = true;
            EnemyIsInside = true;
            Enemy.GetComponent<EnemyController>().EnemyIsInsideTheZone = EnemyIsInside;
        }
        if (collision.tag == "Player")
        {
            PlayerIsInside = true;
            EnemyIsVisited = true;
            Enemy.GetComponent<EnemyController>().EnemyIsAwake = EnemyIsVisited;
            Enemy.GetComponent<EnemyController>().PlayerIsInsideTheZone = PlayerIsInside;
        }
    }
    #endregion
}
