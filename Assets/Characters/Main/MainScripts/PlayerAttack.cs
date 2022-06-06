using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    public GameObject Player;
    public Animator MainAnimator;


    public LayerMask EnemyLayer;
    public float HitBoxRadius;
    public Transform HitboxCenter;

    public GameObject Fireball;
    public Transform FireBallStartPoint;

     private float CurrentStamina; //comes from PlayerStamina
     private bool OnTheGround; //comes from PlayerMovement

    GameObject KillCheck;


    void Update()
    {
        OnTheGround=Player.GetComponent<PlayerMovement>().OnTheGround;
        CurrentStamina=Player.GetComponent<PlayerStamina>().CurrentStamina;
        if (Input.GetMouseButtonDown(0) && CurrentStamina >= 10)
        {
            MainAnimator.SetBool("Attack", true);
        }
        if (Input.GetMouseButtonDown(1) && !OnTheGround && CurrentStamina >= 30)
        {
            MainAnimator.SetTrigger("SuperAttack");
        }

        if (Input.GetKeyDown(KeyCode.E) && OnTheGround && CurrentStamina >= 40)
        {
            MainAnimator.SetBool("Cast", true);
        }
    }
    public void AttackFunc(float Damage) //animation function
    {
        Collider2D[] Enemy = Physics2D.OverlapCircleAll(HitboxCenter.position, HitBoxRadius, EnemyLayer);

        foreach (Collider2D SingleEnemy in Enemy)
        {
            SingleEnemy.GetComponent<EnemyHealth>().EnemyDamage(Damage);
            if (SingleEnemy.GetComponent <EnemyHealth>().EnemyCurrentHealth<=0)
            {
                if (KillCheck!=SingleEnemy.gameObject) // so that multiple combo attacks on a dead enemy dont encrease the score
                {
                    GetComponent<ProgressManager>().KillPoints += SingleEnemy.GetComponent<EnemyHealth>().EnemyMaxHealth / 20;
                    GetComponent<ProgressManager>().Totalkills += 1;
                }
                KillCheck = SingleEnemy.gameObject;
            }
        }
    }

    void FireBall() //animation function
    {
        Instantiate(Fireball, FireBallStartPoint.position, FireBallStartPoint.rotation);
        MainAnimator.SetBool("Cast", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitboxCenter.position, HitBoxRadius);
    }
}
