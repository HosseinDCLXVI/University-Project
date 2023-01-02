using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyController EnemyControllerScript;
    [SerializeField] private LayerMask PlayerLayer;
    [Range(0f, 10f)]
    [SerializeField] private float Attack_Delay;

    [Header("Melee Attack Variables")]
    [SerializeField] private Transform HitBoxCenter;
    [Range(0f, 5f)]
    [SerializeField] private float HitBoxRadius;

    [Header("Ranged Attack Variables")]
    [Range(3f, 10f)]
    [SerializeField] private float EnemyAttackRange;
    [SerializeField] private GameObject FireBall;
    [SerializeField] private Transform FireBallStartPoint;


    private Animator EnemyAnimator;
    private bool CanAttack = true;
    //private bool CloseEnoughToAttack;

    #region General Scripts  
    void Start()
    {
        EnemyAnimator=GetComponent<Animator>();
    }
    void Update()
    {
        AttackController();
        SyncData();
        if (EnemyControllerScript.IsRanged)
        {
            CheckIfCanAttack();
        }
    }
    void SyncData()
    {
        GetComponent<EnemyController>().EnemyAttackRange = EnemyAttackRange;
    }
    void AttackController()
    {
        if (EnemyControllerScript.CloseEnoughToAttack && CanAttack)
        {
            Invoke("AttackDelay", Attack_Delay);
            CanAttack = false;
        }
    }
    void AttackDelay()
    {
        EnemyAnimator.SetBool("Attack", true);
    }
    void StopAttack()
    {
        EnemyAnimator.SetBool("Attack", false);
        CanAttack = true;
    }
    #endregion
    #region Melee Attack
    private void OnDrawGizmos()
    {
        if (EnemyControllerScript.IsMelee)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(HitBoxCenter.position, HitBoxRadius);
        }
    }

    void MeleeAttackDamage(int Damage)
    {
        Collider2D Player = Physics2D.OverlapCircle(HitBoxCenter.position, HitBoxRadius, PlayerLayer);

        if (Player != null)
            Player.GetComponent<PlayerHealth>().PlayerDamage(Damage);
    }


    private void OnTriggerEnter2D(Collider2D collision)//enemy stops movingg when its near the player and attacks
    {
        if (EnemyControllerScript.IsMelee)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.GetComponent<ProgressManager>().PlayerIsVisible)
                {
                    EnemyControllerScript.CloseEnoughToAttack = true;
                    EnemyAnimator.SetBool("Melee", true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//enemy starts movingg when its far from the player
    {
        if (EnemyControllerScript.IsMelee)
        {
            if (collision.CompareTag("Player"))
            {
                EnemyControllerScript.CloseEnoughToAttack = false;
                EnemyAnimator.SetBool("Melee", false);
            }
        }
    }
    #endregion
    #region Ranged Attack
    void CheckIfCanAttack()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), EnemyAttackRange, PlayerLayer);
        if (raycastHit)
        {
            //CanAttackThePlayer = true;
            if(raycastHit.collider.GetComponent<ProgressManager>().PlayerIsVisible)
            EnemyControllerScript.CloseEnoughToAttack = true;
        }
        else
        {
            EnemyControllerScript.CloseEnoughToAttack = false;
        }
    }

    void FireBallGenerator()
    {
        Instantiate(FireBall, new Vector3( FireBallStartPoint.position.x,FireBallStartPoint.transform.position.y,transform.position.z), transform.rotation);
    }
    #endregion
}
