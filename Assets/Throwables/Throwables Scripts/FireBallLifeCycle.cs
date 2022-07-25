using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallLifeCycle : MonoBehaviour
{
    public float Radious;
    public float FireBallAge;
    public Animator FireBallAnimator;
    public Vector3 Offset;
    public LayerMask WallAndGround;
    private void FixedUpdate()
    {
        FireBallCollision();
        Invoke("FireBallEndMovement", FireBallAge);

    }
    public void FireBallCollision()
    {
        Collider2D Wall = Physics2D.OverlapCircle(transform.position + Offset, Radious, WallAndGround);
        if (Wall != null)
        {
            Destroy(this.gameObject);
        }
    }
    void FireBallEndMovement()
    {
        FireBallAnimator.SetBool("End", true);
    }
    public void DestroyFireBall()
    {
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + Offset, Radious);
    }
}
