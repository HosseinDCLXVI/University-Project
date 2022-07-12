using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public Animator EnemyAnimator;
    public Transform HitBoxCenter;
    public LayerMask PlayerLayer;
    public float HitBoxRadius;
    public bool CanAttack =true;
    public bool NearPlayer;//Comes from EnemyPatrol Script
    void Update()
    {
        AttackController();
    }
    void AttackController()
    {
        if(NearPlayer&&CanAttack)
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
}
