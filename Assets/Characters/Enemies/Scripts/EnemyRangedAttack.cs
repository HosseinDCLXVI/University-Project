using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField]private LayerMask Player;
    [SerializeField]private float EnemyVisionDistance;
    [SerializeField]private float Attack_Delay;
    [SerializeField]private GameObject FireBall;

    private bool CanAttackThePlayer=true;
    private Animator EnemyAnimator;
    [HideInInspector]public bool NearPlayer;
    void Start()
    {
        EnemyAnimator=GetComponent<Animator>();
    }

    void Update()
    {
        SearchForPlayer();
        AttackController();
        KeepDistanceFromPlayer();
        NearPlayer = GetComponent<EnemyPatrol>().CanAttackThePlayer;
    }
    void SearchForPlayer()
    {
        RaycastHit2D raycastHit= Physics2D.Raycast(transform.position,transform.TransformDirection(Vector2.left),EnemyVisionDistance, Player);
        if(raycastHit)
        {
            CanAttackThePlayer = true;
            GetComponent<EnemyPatrol>().CanAttackThePlayer = true;
        }    
        else
        {
            GetComponent<EnemyPatrol>().CanAttackThePlayer = false;
        }
    }
    void KeepDistanceFromPlayer()
    {

    }
    void AttackController()
    {
        if (NearPlayer && CanAttackThePlayer)
        {
            Invoke("AttackDelay", Attack_Delay);
            CanAttackThePlayer = false;
        }
    }

    void AttackDelay()
    {
        EnemyAnimator.SetBool("Attack", true);
    }
    void StopAttack()
    {
        EnemyAnimator.SetBool("Attack", false);
        CanAttackThePlayer = true;
    }
    void FireBallGenerator()
    {
        Instantiate(FireBall, transform.position, transform.rotation);
    }    
}