using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    public Vector2 FireBallSpeed;

    void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        transform.Translate(FireBallSpeed * Time.deltaTime);
    }
}
