using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    #region Inspector Variables
    [SerializeField] private EnemyController EnemyControllerScript;
    [SerializeField] private Transform EnemyHealthCanvas;

    [Header("Moving Speed Settings")]
    [SerializeField] private Vector2 EnemyWalkingSpeed;
    [SerializeField] private Vector2 BackwardWalkingSpeed;
    [Range(0f, 20f)]
    [SerializeField] private float InZoneTeleportDelay;

    [Header("Vision Settings")]
    [SerializeField] private LayerMask PlayerLayer;
    [Range(3f, 10f)]
    [SerializeField] private float EnemyVisionDistance;
    [Range(1f, 4f)]
    [SerializeField] private float EnemyBackwardVisionDistance;


    #endregion

    #region Patrol Variables
    private Animator EnemyAnimator;
    private bool EnemyDirection, Right = true, Left = false;
    private bool EnemyCanSeeThePlayer;
    private bool CanMove;
    private bool EnemyIsAwareOfThePlayer;
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

    private void Update()
    {
        if (EnemyIsAwake)
        {
            EnemyAnimator.SetBool("BackToLife", true);
        }

        SyncDataWithControlScript();
        CheckIfEnemyCanSeeThePlayer();
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
        if (EnemyIsAwareOfThePlayer)
        {
            if (PlayerIsInsideTheZone && EnemyIsInsideTheZone)
            {
                if (PlayerPositionInTheZone - EnemyPositionInTheZone > 0)
                {
                    EnemyDirection = Right;
                }
                else if (PlayerPositionInTheZone - EnemyPositionInTheZone < 0)
                {
                    EnemyDirection = Left;
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
            EnemyDirection = !EnemyDirection;

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
            if (EnemyDirection == Right)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                EnemyHealthCanvas.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (EnemyDirection == Left)
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
        RaycastHit2D RaycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), EnemyVisionDistance, PlayerLayer);
        if (RaycastHit)
        {
            EnemyCanSeeThePlayer = true;
            EnemyIsAwareOfThePlayer = true;
        }
        else
        {
            EnemyCanSeeThePlayer = false;
        }
        if (!EnemyControllerScript.PlayerIsInsideTheZone && !EnemyCanSeeThePlayer)
        {
            EnemyIsAwareOfThePlayer = false;
        }

        RaycastHit2D BackwardRaycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), EnemyBackwardVisionDistance, PlayerLayer);
        if (BackwardRaycastHit)
        {
            if (PlayerIsInsideTheZone && EnemyIsInsideTheZone)
            {
                EnemyCanSeeThePlayer = true;
                EnemyIsAwareOfThePlayer = true;
            }
        }
    }
    #endregion
    #region KeepDistanceFromPlayer
    void KeepDistanceFromPlayer()
    {
        if (EnemyIsAwareOfThePlayer)
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
        if (EnemyControllerScript.ZonesLeftBorder >= transform.position.x && GotOutOfZoneBackwards)
        {
            TimeInTheBorder += Time.deltaTime;
            if (TimeInTheBorder >= InZoneTeleportDelay)
            {
                EnemyAnimator.Play("Vanish");
                TimeInTheBorder = 0;
                Invoke("TeleportToRight", 0.5f);
            }
        }
        else if (EnemyControllerScript.ZonesRightBorder <= transform.position.x && GotOutOfZoneBackwards)
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
        EnemyDirection = !EnemyDirection;
        GotOutOfZoneBackwards = false;
        EnemyAnimator.Play("GhostAppears");

    }
    void TeleportToLeft()
    {
        transform.position = new Vector2(EnemyControllerScript.ZonesLeftBorder, transform.position.y);
        EnemyDirection = !EnemyDirection;
        GotOutOfZoneBackwards = false;
        EnemyAnimator.Play("GhostAppears");
    }
    #endregion

    private void ChangeBodyType(RigidbodyType2D rigidbodyType2D)
    {
        Rigidbody2D EnemyRigidbody2D = GetComponent<Rigidbody2D>();
        EnemyRigidbody2D.bodyType = rigidbodyType2D;
    }
}





