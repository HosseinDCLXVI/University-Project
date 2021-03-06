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

    [Space(5)]
    [Header("Ranged Attack Settings")]
    [SerializeField] private GameObject FireballBar;
    [SerializeField] private GameObject Fireball;
    [SerializeField] private Transform FireBallStartPoint;
    [SerializeField] private int MaxFirePoint;
    [SerializeField] private GameObject Arrow;
    [SerializeField] private Transform ArrowStartPoint;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private float AimingDistanceLimit;
    [SerializeField] private float AimAngleLimit;
    [SerializeField] private GameObject Crosshair;
    [SerializeField]private GameObject  AimFieldMesh;
    [SerializeField] private float ArrowForce;


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
    Vector3 randomNumber = new Vector3(0, 0, 0);

    private bool CanShoot=true;
    float SlomoAimingEffect;///
    bool IsSlomo=false;




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
        FirePointControl();
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

        if (Input.GetKeyDown(FireBallButton) && IsOnTheGround && ProgressManagerScript.CurrentFirePoint == MaxFirePoint)
        {
            MainAnimator.SetBool("Cast", true);
        }
        if (ProgressManagerScript.CanAim)
        {
            if (Input.GetKeyDown(AimAndShoot))
            {
                Cursor.visible = false;

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
            if (Input.GetKey(AimAndShoot) && CanShoot)
            {

                SlowmoAiming();
                MainAnimator.SetBool("Aim", true);
                CheckIfCanAim();
                AimFieldMesh.SetActive(true);
                ProgressManagerScript.CanMove = false;

            }
            else if (!Input.GetKey(AimAndShoot) && !Crosshair.activeSelf) //cancel aiming---- crosshair deactivates when its out of range
            {
                MainAnimator.SetBool("Aim", false);
                AimFieldMesh.SetActive(false);
                IsSlomo = false;
                SlowmoAiming();
            }
            else  //shooting ===== canmove will set to true in shooting animation
            {

                MainAnimator.SetBool("Aim", false);

                if (Crosshair.activeSelf)
                    ShootingArrows();


                CanShoot = false;
                if (!IsInvoking("ShootingCoolDown"))
                {
                    Invoke("ShootingCoolDown", 0.5f);
                }

                Crosshair.SetActive(false);
                IsSlomo = false;
                SlowmoAiming();
            }
            Crosshair.transform.position += new Vector3(Input.GetAxis("Mouse X") * 4 + randomNumber.x, Input.GetAxis("Mouse Y") * 4 + randomNumber.y, 0);

            if (Input.GetKeyUp(AimAndShoot))
            {
                ProgressManagerScript.CanMove = true;
            }
        }
    }
    public void StartSlomoAiming()
    {
                IsSlomo = true;
    }
    void SlowmoAiming()
    {

        if(IsSlomo)
        {
            if(SlomoAimingEffect == 1)
             SlomoAimingEffect = 0.2F;

            if (SlomoAimingEffect<1)
            {
                SlomoAimingEffect+=Time.deltaTime/1.5f;
            }
            else
            {
                IsSlomo =false;
                SlomoAimingEffect = 1F;
            }
        }
        else
        {
             SlomoAimingEffect = 1F;
        }
        Time.timeScale = SlomoAimingEffect;
    }
    void ShootingCoolDown()
    {
        CanShoot = true;
    }

    void ShootingArrows()
    {
        Vector3 PlayerPosition = transform.position;
        Vector3 CrosshairDistanceFromPlayer = CrosshairPossitionInWorld() - PlayerPosition;
        float ShootingAngle = Mathf.Atan2(CrosshairDistanceFromPlayer.y, CrosshairDistanceFromPlayer.x) * Mathf.Rad2Deg;

        Quaternion ArrowRotation=new Quaternion (0f,0f,ShootingAngle,0f);
        Arrow = Instantiate(Arrow, ArrowStartPoint.position, ArrowRotation);
        Arrow.SetActive(true);
        Rigidbody2D ArrowRigidbody = Arrow.GetComponent<Rigidbody2D>();
        ArrowRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ArrowRigidbody.rotation = ShootingAngle;
        ArrowRigidbody.AddRelativeForce(new Vector2(ArrowForce, 0f));
    }

    Vector3 CrosshairPossitionInWorld()
    {
        Vector3 CrosshairPosition = Crosshair.transform.position;
        CrosshairPosition.z = 10;

        Vector3 CrosshairPositionInWorld = MainCamera.ScreenToWorldPoint(CrosshairPosition);
        return CrosshairPositionInWorld;
    }
     void CheckIfCanAim()
    {

        Vector3 PlayerPosition = transform.position;
        Vector3 CrosshairDistanceFromPlayer = CrosshairPossitionInWorld() - PlayerPosition;
        float ShootingAngle = Mathf.Atan2(CrosshairDistanceFromPlayer.y, CrosshairDistanceFromPlayer.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(CrosshairDistanceFromPlayer.x)<AimingDistanceLimit && ((ProgressManagerScript.CharacterDirection==Right)&&Mathf.Abs(ShootingAngle) < AimAngleLimit || (ProgressManagerScript.CharacterDirection==Left&&Mathf.Abs(ShootingAngle) > (180 - AimAngleLimit))))
        {
            AimingInstability(CrosshairDistanceFromPlayer);
            Crosshair.SetActive(true);
        }
        else
        {
            Crosshair.SetActive(false);
        }

    }
     void AimingInstability(Vector3 CrosshairDistanceFromPlayer) //madeup formula to make random yet kinda predictable numbers :)
    {
        if (timer <= AimingDificulty_Direction)
        {
            timer += Time.deltaTime;
        }
        else
        {
            randomNumber = new Vector3(Random.Range(-AimingDificulty_Speed.x* CrosshairDistanceFromPlayer.x, AimingDificulty_Speed.x* CrosshairDistanceFromPlayer.x), Random.Range(-AimingDificulty_Speed.y * CrosshairDistanceFromPlayer.x, AimingDificulty_Speed.y * CrosshairDistanceFromPlayer.x));
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
            ProgressManagerScript.CurrentFirePoint += 2;
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

    void FirePointControl()
    {
        if(ProgressManagerScript.CurrentFirePoint>MaxFirePoint)
        {
            ProgressManagerScript.CurrentFirePoint = MaxFirePoint;
        }
        FireballBar.GetComponent<DisplayFirePoint>().MaxFirePoint = MaxFirePoint;
        FireballBar.GetComponent<DisplayFirePoint>().CurrentFirePoint = ProgressManagerScript.CurrentFirePoint;
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
