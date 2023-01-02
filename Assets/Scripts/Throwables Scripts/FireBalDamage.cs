using UnityEngine;

public class FireBalDamage : MonoBehaviour
{
    public float Radious;
    public Vector3 Offset;
    public LayerMask Target;
    public int Damage;
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
                DamageTarget.GetComponent<PlayerHealth>().PlayerDamage(Damage);
            }
            else
                DamageTarget.GetComponent<EnemyHealth>().EnemyDamage(Damage);

            Destroy(this.gameObject);
        }
    }
}
