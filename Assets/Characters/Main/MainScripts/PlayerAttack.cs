using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class PlayerAttack : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] private ProgressManager ProgressManagerScript;
    [Header("Attack Inputs")]
    [SerializeField] private KeyCode LightAttackButton;
    [SerializeField] private KeyCode HeavyAttackButton;
    [SerializeField] private KeyCode FireBallButton;
    [SerializeField] private KeyCode AimAndShoot;

    [Space(5)]
    [Header("Melee Attack Settings")]
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private float HitBoxRadius;
    [SerializeField] private Transform HitboxCenter;
    [SerializeField] private int LightAttackRquiredStamina;
    [SerializeField] private int HeavyAttackRquiredStamina;
    [SerializeField] private int FireBallRquiredStamina;

    [Space(5)]
    [Header("Ranged Attack Settings")]
    [SerializeField] private GameObject Fireball;
    [SerializeField] private Transform FireBallStartPoint;

    [SerializeField] private GameObject Arrow;
    [SerializeField] private Transform ArrowStartPoint;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private float AimingDistanceLimit;
    [SerializeField] private float AimAngleLimit;
    [SerializeField] private GameObject Crosshair;


    [Tooltip("controllable Movements Speed")]
    [SerializeField] private float crosshairSpeed;
    [Tooltip("uncontrollable Movements Speed")]
    [SerializeField] private Vector2 AimingDificulty_Speed;
    [Tooltip("uncontrollable Moving Direction Cahnges")]
    [SerializeField] private float AimingDificulty_Direction;
    #endregion

    #region Other Variables
     private Animator MainAnimator;
     private bool  Right = true, Left = false;
    private float CurrentStamina; //comes from PlayerStamina
     private bool IsOnTheGround; //comes from PlayerMovement
    private bool CrossHairIsActive;
    Vector3 randomNumber = new Vector3(0, 0, 0);


    private float timer=0;
    GameObject KillCheck;
    #endregion

    private void Start()
    {
        MainAnimator = GetComponent<Animator>();
    }

    #region Input Control
    void Update()
    {
        IsOnTheGround=ProgressManagerScript.IsOnTheGround;
        CurrentStamina=ProgressManagerScript.CurrentStamina;
        InputControl();
        CheckIfCanAim();
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

        if(Input.GetKeyDown(AimAndShoot))
        {
            if (ProgressManagerScript.CharacterDirection == Right)
            {
                Vector3 CrosshairStartPointInWorld = new Vector3((transform.position.x + (transform.position.x + AimingDistanceLimit)) / 2, transform.position.y, transform.position.z);
                Vector3 CrosshairStartPointInScreen = MainCamera.WorldToScreenPoint(CrosshairStartPointInWorld);
                Crosshair.transform.position = CrosshairStartPointInScreen;
            }
            else
            {
                Vector3 CrosshairStartPointInWorld = new Vector3((transform.position.x + (transform.position.x - AimingDistanceLimit)) / 2, transform.position.y, transform.position.z);
                Vector3 CrosshairStartPointInScreen = MainCamera.WorldToScreenPoint(CrosshairStartPointInWorld);
                Crosshair.transform.position = CrosshairStartPointInScreen;
            }
        }
        if(Input.GetKey(AimAndShoot)&& CheckIfCanAim())
        {
            MainAnimator.SetBool("Aim", true);            
            Crosshair.SetActive(true);
        }
        else
        {
            MainAnimator.SetBool("Aim", false);
            Crosshair.SetActive(false);
        }
        Crosshair.transform.position += new Vector3(Input.GetAxis("Mouse X") * 4 + randomNumber.x, Input.GetAxis("Mouse Y") * 4 + randomNumber.y, 0);

        if (Input.GetKeyUp(AimAndShoot) && CheckIfCanAim())
        {
            MainAnimator.SetBool("Aim", true);// should it be true or this line should be removed
            MainAnimator.SetTrigger("Shoot");
            //triangle
            // create arrow for shooting
            //fix the animations
            //arrow effect while shooting

            //complete the level
            //make the last enemy
        }

    }

    bool CheckIfCanAim()
    {
        Cursor.visible = false;

        Vector3 CrosshairPosition = Crosshair.transform.position;
        CrosshairPosition.z = 10;

        CrosshairScript crosshairScript = Crosshair.GetComponent<CrosshairScript>();
        Vector3 CrosshairPositionInWorld = MainCamera.ScreenToWorldPoint(CrosshairPosition);

        Vector3 PlayerPosition = transform.position;
        Vector3 CrosshairDistanceFromPlayer = CrosshairPositionInWorld - PlayerPosition;
        float ShootingAngle = Mathf.Atan2(CrosshairDistanceFromPlayer.y, CrosshairDistanceFromPlayer.x) * Mathf.Rad2Deg;


        if (Mathf.Abs(CrosshairDistanceFromPlayer.x)<AimingDistanceLimit && ((ProgressManagerScript.CharacterDirection==Right)&&Mathf.Abs(ShootingAngle) < AimAngleLimit || (ProgressManagerScript.CharacterDirection==Left&&Mathf.Abs(ShootingAngle) > (180 - AimAngleLimit))))
        {
            RandomNumberGeneratorForCrossHairRandomMovement();
            return true;
        }
        else
        {
            return false;
        }

    }
    void RandomNumberGeneratorForCrossHairRandomMovement() //madeup formula to make random yet kinda predictable numbers :)
    {
        if (timer <= AimingDificulty_Direction)
        {
            timer += Time.deltaTime;
        }
        else
        {
          // randomNumber = new Vector3(Random.Range(-AimingDificulty_Speed.x, AimingDificulty_Speed.x), Random.Range(-AimingDificulty_Speed.y, AimingDificulty_Speed.y)); //-------this line is good enough to make random movements but they are not  predictable enough

             if (randomNumber.x <= 0)//------- this code makes more predictable numbers
             {
                 if (randomNumber.y <= 0)
                     randomNumber = new Vector3(Random.Range(0, AimingDificulty_Speed.x), Random.Range(0, AimingDificulty_Speed.y));
                 else if(randomNumber.y > 0)
                     randomNumber = new Vector3(Random.Range(0, AimingDificulty_Speed.x), Random.Range(-AimingDificulty_Speed.y, 0));
             }
             else if (randomNumber.x > 0)
             {
                 if(randomNumber.y <= 0)
                     randomNumber = new Vector3(Random.Range(-AimingDificulty_Speed.x,0), Random.Range(0, AimingDificulty_Speed.y));
                 else if(randomNumber.y>0)
                     randomNumber = new Vector3(Random.Range(-AimingDificulty_Speed.x, 0), Random.Range(-AimingDificulty_Speed.y, 0));
             }

             if (Mathf.Abs(randomNumber.x)+ Mathf.Abs(randomNumber.x) < (AimingDificulty_Speed.x+ AimingDificulty_Speed.y) /10) 
             {
                 randomNumber = new Vector3(Random.Range(-AimingDificulty_Speed.x, AimingDificulty_Speed.x), Random.Range(-AimingDificulty_Speed.y, AimingDificulty_Speed.y));
             }
            timer = 0;
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
