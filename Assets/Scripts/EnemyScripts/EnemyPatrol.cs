using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    #region Inspector Variables
    [SerializeField] private bool GuardPost;
    [SerializeField] private EnemyController EnemyControllerScript;
    [SerializeField] private Transform EnemyHealthCanvas;

    [Header("Moving Speed Settings")]
    [SerializeField] private Vector2 EnemyWalkingSpeed;
    [SerializeField] private Vector2 BackwardWalkingSpeed;
    [Range(0f, 20f)]
    [SerializeField] private float InZoneTeleportDelay;

    [Header("Vision Settings")]
    [SerializeField] private LayerMask PlayerLayer;
    //[Range(3f, 10f)]
    //[SerializeField] private float EnemyVisionDistance;
    [Range(1f, 4f)]
    [SerializeField] private float EnemyBackwardVisionDistance;

    [SerializeField] public bool PlayerShouldStayUndetected;

    #endregion

    #region Patrol Variables
    private Animator EnemyAnimator;
    private bool Right = true, Left = false;
    //private bool EnemyCanSeeThePlayer;
    private bool CanMove;
    //[HideInInspector] public bool EnemyIsAwareOfThePlayer;
    private bool GotOutOfZoneBackwards;
    private float TimeInTheBorder = 0;
    #endregion


    #region Attack Variables
    private bool CloseEnoughToAttack;
    #endregion

    #region zone Variables
    private bool EnemyIsAwake = false;
    private bool PlayerIsInsideTheZone;
    private bool EnemyIsInsideTheZone;
    private float PlayerPositionInTheZone;
    private float EnemyPositionInTheZone;
    #endregion
    private void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
    }
    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        EnemyIsAwake = EnemyControllerScript.EnemyIsAwake;
        if (EnemyIsAwake)
        {
            EnemyAnimator.Play("Idle");
        }
    }
    private void Update()
    {
        if (EnemyIsAwake)
        {
            EnemyAnimator.SetBool("BackToLife", true);
        }

        SyncDataWithControlScript();

        // checks if enemy can see the player by sending a ray cast
        CheckIfEnemyCanSeeThePlayer();


        if(!GuardPost)
        EnemyFolowThePlayer();

        if (EnemyControllerScript.CanWalkBackward && EnemyControllerScript.IsRanged)
        {
            KeepDistanceFromPlayer();
        }

        if (EnemyControllerScript.CanTeleportInTheZone)
        {
            InZoneTeleport();
        }
    }
    void FixedUpdate()
    {
        WalkingFunc();
    }
    #region General Patrol Functions
    private void SyncDataWithControlScript()
    {
        CloseEnoughToAttack = EnemyControllerScript.CloseEnoughToAttack;

        #region zone Variables
        EnemyIsAwake = EnemyControllerScript.EnemyIsAwake;
        PlayerIsInsideTheZone = EnemyControllerScript.PlayerIsInsideTheZone;
        EnemyIsInsideTheZone = EnemyControllerScript.EnemyIsInsideTheZone;
        PlayerPositionInTheZone = EnemyControllerScript.PlayerPositionInTheZone;
        EnemyPositionInTheZone = EnemyControllerScript.EnemyPositionInTheZone;
        #endregion
    }

    void EnemyFolowThePlayer()
    {
        if (EnemyControllerScript.EnemyIsAwareOfThePlayer)
        {
            if (PlayerIsInsideTheZone && EnemyIsInsideTheZone)
            {
                if (PlayerPositionInTheZone - EnemyPositionInTheZone > 0)
                {
                    EnemyControllerScript.EnemyDirection = Right;
                }
                else if (PlayerPositionInTheZone - EnemyPositionInTheZone < 0)
                {
                    EnemyControllerScript.EnemyDirection = Left;
                }
            }
            else if (!PlayerIsInsideTheZone && !EnemyIsInsideTheZone)
            {
                CanMove = false;
            }
            else
            {
                CanMove = true;
            }
        }
        else
        {
            CanMove = true;
        }


        if (!EnemyIsInsideTheZone && CanMove)
        {
            CanMove = false;
            Invoke("ReturnEnemyToTheZone", 1f);
        }
    }

    void ReturnEnemyToTheZone()
    {
        if (EnemyIsInsideTheZone)
            return;
        ///
        if (!GotOutOfZoneBackwards)
            EnemyControllerScript.EnemyDirection = !EnemyControllerScript.EnemyDirection;

        CanMove = true;
        EnemyIsInsideTheZone = true;
        EnemyControllerScript.EnemyIsInsideTheZone = EnemyIsInsideTheZone;
    }
    void WalkingFunc()
    {
        if (!CloseEnoughToAttack && EnemyIsAwake && CanMove)
        {
            EnemyAnimator.SetBool("Walk", true);
            transform.Translate(EnemyWalkingSpeed * Time.deltaTime);
            if (EnemyControllerScript.EnemyDirection == Right)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                EnemyHealthCanvas.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (EnemyControllerScript.EnemyDirection == Left)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                EnemyHealthCanvas.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            EnemyAnimator.SetBool("Walk", false);
        }
    }

    void CheckIfEnemyCanSeeThePlayer()
    {

        RaycastHit2D BackwardRaycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), EnemyBackwardVisionDistance, PlayerLayer);
        if(BackwardRaycastHit)
        if (BackwardRaycastHit.collider.GetComponent<ProgressManager>().PlayerIsVisible)
        {
            if (PlayerIsInsideTheZone && EnemyIsInsideTheZone)
            {
                EnemyControllerScript.EnemyCanSeeThePlayer = true;
                EnemyControllerScript.EnemyIsAwareOfThePlayer = true;
                StelthMission(BackwardRaycastHit);
            }
        }
        if (!EnemyControllerScript.PlayerIsInsideTheZone && !EnemyControllerScript.EnemyCanSeeThePlayer && EnemyControllerScript.EnemyIsAwareOfThePlayer)
        {
            if(!IsInvoking("ForgetAboutThePlayer"))
            {
                Invoke("ForgetAboutThePlayer", 5);

            }
        }
    }
    void ForgetAboutThePlayer()
    {
        if (!EnemyControllerScript.PlayerIsInsideTheZone && !EnemyControllerScript.EnemyCanSeeThePlayer && EnemyControllerScript.EnemyIsAwareOfThePlayer)
            EnemyControllerScript.EnemyIsAwareOfThePlayer = false;
    }
    public void StelthMission(RaycastHit2D Player)
    {
        if (PlayerShouldStayUndetected)
        {
            Player.collider.GetComponent<PlayerHealth>().InvokeGameOver();
        }
    }
    #endregion
    #region KeepDistanceFromPlayer
    void KeepDistanceFromPlayer()
    {
        if (EnemyControllerScript.EnemyIsAwareOfThePlayer)
        {
            if (Mathf.Abs(PlayerPositionInTheZone - EnemyPositionInTheZone) <= EnemyControllerScript.EnemyAttackRange)
            {
                if (EnemyControllerScript.ZonesLeftBorder <= transform.position.x && transform.position.x <= EnemyControllerScript.ZonesRightBorder)
                {
                    transform.Translate(-BackwardWalkingSpeed * Time.deltaTime);
                    GotOutOfZoneBackwards = false;
                }
                else
                {
                    GotOutOfZoneBackwards = true;
                }
            }
        }
    }
    #endregion
    #region InZoneTeleport
    void InZoneTeleport()
    {
        if (EnemyControllerScript.ZonesLeftBorder >= transform.position.x && GotOutOfZoneBackwards&&PlayerIsInsideTheZone)
        {
            TimeInTheBorder += Time.deltaTime;
            if (TimeInTheBorder >= InZoneTeleportDelay)
            {
                EnemyAnimator.Play("Vanish");
                TimeInTheBorder = 0;
                Invoke("TeleportToRight", 0.5f);
            }
        }
        else if (EnemyControllerScript.ZonesRightBorder <= transform.position.x && GotOutOfZoneBackwards&&PlayerIsInsideTheZone)
        {
            TimeInTheBorder += Time.deltaTime;
            if (TimeInTheBorder >= InZoneTeleportDelay)
            {
                EnemyAnimator.Play("Vanish");
                TimeInTheBorder = 0;
                Invoke("TeleportToLeft", 0.5f);
            }
        }
        else
        {
            TimeInTheBorder = 0;
        }
    }
    void TeleportToRight()
    {
        transform.position = new Vector2(EnemyControllerScript.ZonesRightBorder, transform.position.y);
        EnemyControllerScript.EnemyDirection = !EnemyControllerScript.EnemyDirection;
        GotOutOfZoneBackwards = false;
        EnemyAnimator.Play("GhostAppears");

    }
    void TeleportToLeft()
    {
        transform.position = new Vector2(EnemyControllerScript.ZonesLeftBorder, transform.position.y);
        EnemyControllerScript.EnemyDirection = !EnemyControllerScript.EnemyDirection;
        GotOutOfZoneBackwards = false;
        EnemyAnimator.Play("GhostAppears");
    }
    #endregion

    private void ChangeBodyType(RigidbodyType2D rigidbodyType2D)
    {
        Rigidbody2D EnemyRigidbody2D = GetComponent<Rigidbody2D>();
        if(EnemyRigidbody2D.bodyType!=RigidbodyType2D.Static)
        EnemyRigidbody2D.bodyType = rigidbodyType2D;
    }
}





