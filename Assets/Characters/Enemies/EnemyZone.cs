using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    #region Variables
    [HideInInspector] public GameObject Enemy; //for teleporting script
    [HideInInspector] public bool EnemyIsInside;
    [HideInInspector] public bool PlayerIsInside;
    [HideInInspector] public bool EnemyMovingDirection,Right=true,Left=false;

    [SerializeField] private Vector2 BordersSize;
    [SerializeField] LayerMask EnemyLayer;

    private float PlayerPosition;
    private float EnemyPosition;
    private bool EnemyIsVisited;
    private Collider2D ZoneCollider;
    #endregion

    #region Manage Enemy Movement in the zone
    private void Start()
    {
        ZoneCollider = GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
        EnemyFolowThePlayer();
        Enemy.GetComponent<EnemyPatrol>().Visited = EnemyIsVisited;
    }
    void EnemyFolowThePlayer()
    {
        if (PlayerIsInside && EnemyIsInside)
        {
            if (PlayerPosition - EnemyPosition > 0)
            {
                EnemyMovingDirection = Right;
            }
            else if (PlayerPosition - EnemyPosition < 0)
            {
                EnemyMovingDirection = Left;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyIsInside = false;
            ReturnEnemyToTheZone(collision);
        }
        if (collision.tag == "Player")
        {
            PlayerIsInside = false;
        }
    }

    void ReturnEnemyToTheZone(Collider2D collision)
    {
        Enemy = null;
        collision.GetComponent<CircleCollider2D>().enabled = false;
        EnemyMovingDirection = !EnemyMovingDirection;
        collision.GetComponent<EnemyPatrol>().EnemyDirection = EnemyMovingDirection;
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
            collision.GetComponent<EnemyPatrol>().EnemyDirection = EnemyMovingDirection;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy = collision.gameObject;
            collision.GetComponent<CircleCollider2D>().enabled = true;
            EnemyIsInside = true;
        }
        if (collision.tag == "Player")
        {
            PlayerIsInside = true;
            EnemyIsVisited = true;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector2 RightBorderPosition = new Vector2(transform.position.x + ZoneCollider.bounds.size.x / 2, transform.position.y);
        Vector2 LeftBorderPosition = new Vector2(transform.position.x - ZoneCollider.bounds.size.x / 2, transform.position.y);

        Gizmos.DrawWireCube(RightBorderPosition, BordersSize);
        Gizmos.DrawWireCube(LeftBorderPosition, BordersSize);
    }

    void EnemyReachTheBorder()
    {
        Vector2 RightBorderPosition = new Vector2(transform.position.x + ZoneCollider.bounds.size.x / 2, transform.position.y);
        Vector2 LeftBorderPosition = new Vector2(transform.position.x - ZoneCollider.bounds.size.x / 2, transform.position.y);

        Collider2D[] EnemiesInTheRightBorder = Physics2D.OverlapBoxAll(RightBorderPosition, BordersSize,EnemyLayer);
        foreach (Collider2D Enemy in EnemiesInTheRightBorder)
        {
            
        }

        Collider2D[] EnemiesInTheLeftBorder = Physics2D.OverlapBoxAll(LeftBorderPosition, BordersSize, EnemyLayer);
        foreach (Collider2D Enemy in EnemiesInTheLeftBorder)
        {

        }
    }
    #endregion
}
