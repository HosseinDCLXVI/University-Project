using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    private enum EnemyType {Ghost,Skeleton}
    #region Inspector Variables
    [SerializeField]private EnemyController EnemyControllerScript;
    [SerializeField]private Transform EnemyHealthCanvas;
    [SerializeField]private Vector2 EnemyWalkingSpeed;
    [SerializeField]private Vector2 BackwardWalkingSpeed;
    [SerializeField]private float EnemyVisionDistance;
    [SerializeField]private float EnemyBackwardVisionDistance; //
    [SerializeField]private LayerMask PlayerLayer;
    [SerializeField]private float InZoneTeleportDelay;
    #endregion

    #region Other Variables
    private Animator EnemyAnimator;
    [HideInInspector]public bool EnemyDirection, Right = true, Left = false;// comes from EnemyZone Script
    //[HideInInspector]public bool EnemyInTheRightBorder;
    //[HideInInspector]public bool EnemyInTheLeftBorder;
    [HideInInspector]public bool EnemyCanSeeThePlayer;
    [HideInInspector]public bool CanMove;
    [HideInInspector]public bool EnemyIsAwareOfThePlayer;
    [HideInInspector]public bool GotOutOfZoneBackwards;
    private float TimeInTheBorder = 0;


    #region Attack Variables
    [HideInInspector]public bool CloseEnoughToAttack;
    #endregion

    #region zone Variables
    private bool EnemyIsAwake =false;
     private bool PlayerIsInsideTheZone;
     private bool EnemyIsInsideTheZone;
     private float PlayerPositionInTheZone;
     private float EnemyPositionInTheZone;

    #endregion
    #endregion
    #region Control Enemy Movement
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

        if (EnemyControllerScript.IsRanged)
        {
            KeepDistanceFromPlayer();
        }
        if(EnemyControllerScript.CanTeleportInTheZone)
        {
            InZoneTeleport();
        }
    }
    private void SyncDataWithControlScript()
    {
        CloseEnoughToAttack = EnemyControllerScript.CloseEnoughToAttack;

        #region zone Variables
        EnemyIsAwake = EnemyControllerScript.EnemyIsAwake;
        PlayerIsInsideTheZone =EnemyControllerScript.PlayerIsInsideTheZone;
        EnemyIsInsideTheZone=EnemyControllerScript.EnemyIsInsideTheZone;
        PlayerPositionInTheZone=EnemyControllerScript.PlayerPositionInTheZone;
        EnemyPositionInTheZone = EnemyControllerScript.EnemyPositionInTheZone;
        #endregion
    }
    void FixedUpdate()
    {
        WalkingFunc();
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
        EnemyControllerScript.EnemyIsInsideTheZone= EnemyIsInsideTheZone;       
    }
    void WalkingFunc()
    {
        if (!CloseEnoughToAttack&&EnemyIsAwake&&CanMove)
        {
            EnemyAnimator.SetBool("Walk", true);
            transform.Translate(EnemyWalkingSpeed * Time.deltaTime);
            if (EnemyDirection==Right)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                EnemyHealthCanvas.eulerAngles = new Vector3(0, 0, 0);
            }
            else if(EnemyDirection==Left)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                EnemyHealthCanvas.eulerAngles=new Vector3(0,0, 0);
            }
        }
        else
        {
            EnemyAnimator.SetBool("Walk", false);
        }
    }

    void CheckIfEnemyCanSeeThePlayer()
    {
        RaycastHit2D RaycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), EnemyVisionDistance,PlayerLayer);
        if (RaycastHit)
        {
            EnemyCanSeeThePlayer = true;
            EnemyIsAwareOfThePlayer=true;
        }
        else
        {
            EnemyCanSeeThePlayer = false;
        }
        if(!EnemyControllerScript.PlayerIsInsideTheZone && !EnemyCanSeeThePlayer)
        {
            EnemyIsAwareOfThePlayer = false;
        }

        RaycastHit2D BackwardRaycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), EnemyBackwardVisionDistance,PlayerLayer);
        if(BackwardRaycastHit)
        {
            if (PlayerIsInsideTheZone && EnemyIsInsideTheZone)
            {
                EnemyCanSeeThePlayer = true;
                EnemyIsAwareOfThePlayer = true;
            }
        }
    }
    #endregion
    void KeepDistanceFromPlayer()
    {
        
            if (EnemyIsAwareOfThePlayer)
                if (Mathf.Abs(PlayerPositionInTheZone - EnemyPositionInTheZone) <= EnemyControllerScript.EnemyAttackRange)
                {
                    if (EnemyControllerScript.ZonesLeftBorder <= transform.position.x && transform.position.x<=EnemyControllerScript.ZonesRightBorder)
                    {
                        transform.Translate(BackwardWalkingSpeed * Time.deltaTime);
                        GotOutOfZoneBackwards = false;
                    }
                    else
                    {
                        GotOutOfZoneBackwards=true;
                    }
                }
    }
    void InZoneTeleport()
    {
        if (EnemyControllerScript.ZonesLeftBorder >= transform.position.x && GotOutOfZoneBackwards)
        {
            TimeInTheBorder+=Time.deltaTime;
            if(TimeInTheBorder >=InZoneTeleportDelay)
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

}





