using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour
{
    public float Radious;
    public Vector3 Offset;
    public LayerMask Target;
    void FixedUpdate()
    {
        DamageFunc();
    }
    void DamageFunc()
    {
        Collider2D DamageTarget = Physics2D.OverlapCircle(transform.position + Offset, Radious, Target);
        if (DamageTarget != null)
        {
            if (DamageTarget.tag == "Player")
            {
                int PlayerCurentHealth=(int)DamageTarget.GetComponent<ProgressManager>().CurrentHealth+1;
                DamageTarget.GetComponent<PlayerHealth>().PlayerDamage(PlayerCurentHealth);

            }
            else
            {
                int EnemyCurentHealth = (int)DamageTarget.GetComponent<EnemyController>().EnemyCurrentHealth + 1;
                DamageTarget.GetComponent<EnemyHealth>().EnemyDamage(EnemyCurentHealth);
            }

            Destroy(this.gameObject);
        }
    }
}
