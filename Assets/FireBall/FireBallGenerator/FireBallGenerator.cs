using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallGenerator : MonoBehaviour
{
    public GameObject Fireball;
    public bool Cast;
    public Transform FireBallStartPoint;
    public float Gap=3f;
    public float StartingDelay=0;
    private void Start()
    {
        Cast = true;
    }
    void Update()
    {
        if(StartingDelay==0)
        {
             if(Cast)
             {
                  Cast = false;
                  Invoke("FireBall", Gap);
             }
        }
        else
        {
            Cast = false;
            Invoke("FireBall", StartingDelay);
            StartingDelay = 0;
        }

    }
    void FireBall() //animation function
    {
        Instantiate(Fireball, FireBallStartPoint.position, FireBallStartPoint.rotation);
        Cast = true;
    }
}
