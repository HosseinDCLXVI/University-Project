using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType {Ghost,Skeleton}
    public EnemyType enemyType;
    [HideInInspector]public bool CanTeleportInTheZone, CanTeleportBetweenTheZones, CanPatrol, IsRanged, IsMelee ,CanWalkBackward;

    #region EnemyHealth Variables
    [HideInInspector]public float EnemyMaxHealth;
    [HideInInspector]public float EnemyCurrentHealth;
    #endregion

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
        CanWalkBackward = true;
    }
    void EnemyIsSkeleton()
    {
        CanTeleportInTheZone = false;
        CanTeleportBetweenTheZones = true;
        CanPatrol = true;
        IsRanged = false;
        IsMelee = true;
        CanWalkBackward = false;
    }
}
