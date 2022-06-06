using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    public LayerMask Player;
    public float Distance;
    public Transform StartPoint;
    public bool CanAttack=true;
    public Animator EnemyAnimator;
    public float Attack_Delay;
    public GameObject FireBall;
    public bool NearPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCheck();
        AttackController();
        NearPlayer = GetComponent<EnemyPatrol>().NearPlayer;
    }
    void PlayerCheck()
    {
        RaycastHit2D raycastHit= Physics2D.Raycast(StartPoint.position,StartPoint.TransformDirection(Vector2.left),Distance, Player);
        if(raycastHit)
        {
            GetComponent<EnemyPatrol>().NearPlayer = true;
        }    
        else
        {
            GetComponent<EnemyPatrol>().NearPlayer = false;
        }
    }
    void AttackController()
    {
        if (NearPlayer && CanAttack)
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
    void FireBallGenerator()
    {
        Instantiate(FireBall, StartPoint.position, StartPoint.rotation);
    }    
}