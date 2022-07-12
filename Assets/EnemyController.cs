using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType {Ghost,Skeleton}
    public EnemyType enemyType;
    [SerializeField]private EnemyPatrol EnemyPatrolScript;
    [SerializeField]private EnemyMeleeAttack EnemyAttackScript;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
