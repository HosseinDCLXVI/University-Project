using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType {Ghost,Skeleton}
    public EnemyType enemyType;
    [SerializeField]private EnemyPatrol EnemyPatrolScript;
    [SerializeField]private EnemyAttack EnemyAttackScript;



    #region Attack Variables
    [HideInInspector]public bool CloseEnoughToAttack=false;
    [HideInInspector]public float EnemyAttackRange;
    #endregion

    #region Zone Variables
    public bool EnemyIsAwake = false;
    [HideInInspector]public bool PlayerIsInsideTheZone;
    [HideInInspector]public bool EnemyIsInsideTheZone;
    [HideInInspector]public float PlayerPositionInTheZone;
    [HideInInspector]public float EnemyPositionInTheZone;

    [HideInInspector] public float ZonesRightBorder;
    [HideInInspector] public float ZonesLeftBorder;
    #endregion


    [HideInInspector]public bool CanTeleportInTheZone, CanTeleportBetweenTheZones, CanPatrol, IsRanged, IsMelee;
    void Start()
    {
        if(enemyType==EnemyType.Ghost)
        {
            EnemyIsGhost();
        }
        if(enemyType==EnemyType.Skeleton)
        {
            EnemyIsSkeleton();
        }
    }
    void EnemyIsGhost()
    {
        CanTeleportInTheZone=true;
        CanTeleportBetweenTheZones=true;
        CanPatrol=true;
        IsRanged=true;
        IsMelee = false;
    }
    void EnemyIsSkeleton()
    {
        CanTeleportInTheZone = false;
        CanTeleportBetweenTheZones = true;
        CanPatrol = true;
        IsRanged = false;
        IsMelee = true;
    }
    void Update()
    {
        
    }
}
