using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] private EnemyController EnemyControllerScript;
    public Animator EnemyAnimator;
    public Transform HitBoxCenter;
    public LayerMask PlayerLayer;
    public float HitBoxRadius;
    public bool CanAttack =true;
    public bool CloseEnoughToAttack;
    void Update()
    {
        AttackController();
        SyncData();
    }
    void SyncData()
    {
        GetComponent<EnemyController>().CloseEnoughToAttack = CloseEnoughToAttack;
    }
    void AttackController()
    {
        if(CloseEnoughToAttack&&CanAttack)
        {
            Invoke("AttackDelay", 1);
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitBoxCenter.position,HitBoxRadius);
    }


    void AttackDamage(int Damage)
    {
        Collider2D Player=Physics2D.OverlapCircle(HitBoxCenter.position,HitBoxRadius,PlayerLayer);

        if (Player!=null)
        Player.GetComponent<PlayerHealth>().PlayerDamage(Damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)//enemy stops movingg when near the player and attacks
    {
        if (collision.tag == "Player")
        {
            CloseEnoughToAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//enemy starts movingg when far from the player
    {
        if (collision.tag == "Player")
        {
            CloseEnoughToAttack = false;
        }
    }
}
