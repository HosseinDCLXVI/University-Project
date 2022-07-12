using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    private enum EnemyType {Ghost,Skeleton}
    #region Inspector Variables
    [SerializeField]private EnemyType enemyType;
    [SerializeField]private Transform EnemyHealthCanvas;
    [SerializeField]private Vector2 EnemyWalkingSpeed;
    #endregion
    #region Other Variables
    private Animator EnemyAnimator;
    [HideInInspector]public bool EnemyDirection, Right = true, Left = false;// comes from EnemyZone Script
    [HideInInspector]public bool CanAttackThePlayer;
    [HideInInspector]public bool Visited =false;
    #endregion
    #region Control Enemy Movement
    private void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        EnemyMovement();
        if(enemyType == EnemyType.Skeleton)
        {
            Debug.Log("dfs");
        }
        if(Visited)
        {
            EnemyAnimator.SetBool("BackToLife", true);
        }
    }
    void EnemyMovement()
    {
        if (!CanAttackThePlayer&&Visited)
        {
            EnemyAnimator.SetBool("Walk", true);
            transform.Translate(EnemyWalkingSpeed * Time.deltaTime);
            if (EnemyDirection==Right)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                EnemyHealthCanvas.eulerAngles = new Vector3(0, 0, 0);
            }
            else if(EnemyDirection==Left)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                EnemyHealthCanvas.eulerAngles=new Vector3(0,0, 0);
            }
        }
        else
        {
            EnemyAnimator.SetBool("Walk", false);
        }
    }
    #endregion
    #region Check if enemy can attack the player
    private void OnTriggerEnter2D(Collider2D collision)//enemy stops movingg when near the player and attacks
    {
        if (collision.tag == "Player")
        {
            EnemyMeleeAttack enemy = GetComponent<EnemyMeleeAttack>();
            if (enemy != null)
            GetComponent<EnemyMeleeAttack>().NearPlayer = true;
            CanAttackThePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//enemy starts movingg when far from the player
    {
        if (collision.tag == "Player")
        {
            EnemyMeleeAttack enemy = GetComponent<EnemyMeleeAttack>();
            if (enemy != null)
            GetComponent<EnemyMeleeAttack>().NearPlayer = false;
            CanAttackThePlayer = false;
        }
    }
    #endregion
}





