using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Inspector Variables
    [Header("Attack Inputs")]
    [SerializeField] private KeyCode LightAttackButton;
    [SerializeField] private KeyCode HeavyAttackButton;
    [SerializeField] private KeyCode FireBallButton;

    [Space(5)]
    [Header("Melee Attack Settings")]
    [SerializeField]private LayerMask EnemyLayer;
    [SerializeField]private float HitBoxRadius;
    [SerializeField]private Transform HitboxCenter;
    [SerializeField] private int LightAttackRquiredStamina;
    [SerializeField] private int HeavyAttackRquiredStamina;
    [SerializeField]private int FireBallRquiredStamina;

    [Space(5)]
    [Header("Ranged Attack Settings")]
    [SerializeField]private GameObject Fireball;
    [SerializeField]private Transform FireBallStartPoint;
    #endregion

    #region Other Variables
     private Animator MainAnimator;

     private float CurrentStamina; //comes from PlayerStamina
     private bool IsOnTheGround; //comes from PlayerMovement

    GameObject KillCheck;
    #endregion

    private void Start()
    {
        MainAnimator = GetComponent<Animator>();
    }

    #region Input Control
    void Update()
    {
        IsOnTheGround=GetComponent<PlayerMovement>().IsOnTheGround;
        CurrentStamina=GetComponent<PlayerStamina>().CurrentStamina;
        InputControl();
    }
    private void InputControl()
    {
        if (Input.GetKeyDown(LightAttackButton) && CurrentStamina >= LightAttackRquiredStamina)
        {
            MainAnimator.SetBool("Attack", true);
        }
        if (Input.GetKeyDown(HeavyAttackButton) && !IsOnTheGround && CurrentStamina >= HeavyAttackRquiredStamina)
        {
            MainAnimator.SetTrigger("SuperAttack");
        }

        if (Input.GetKeyDown(FireBallButton) && IsOnTheGround && CurrentStamina >= FireBallRquiredStamina)
        {
            MainAnimator.SetBool("Cast", true);
        }
    }
    #endregion

    #region Main Attack Functions (They get called from the inside of the animations)
    public void AttackFunc(float Damage) //animation function
    {
        Collider2D[] Enemy = Physics2D.OverlapCircleAll(HitboxCenter.position, HitBoxRadius, EnemyLayer);

        foreach (Collider2D SingleEnemy in Enemy)
        {
            SingleEnemy.GetComponent<EnemyHealth>().EnemyDamage(Damage);
            if (SingleEnemy.GetComponent <EnemyController>().EnemyCurrentHealth<=0)
            {
                if (KillCheck!=SingleEnemy.gameObject) // so that multiple combo attacks on a dead enemy dont encrease the score
                {
                    GetComponent<ProgressManager>().KillPoints += SingleEnemy.GetComponent<EnemyController>().EnemyMaxHealth / 20;
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
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitboxCenter.position, HitBoxRadius);
    }
}
