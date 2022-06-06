using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    //public GameObject RightBackground;
    //public GameObject LeftBackGround;
    public float MovementSpeed, Length, StartPoint,startpointy;//,RightStartPoint,LeftStartPoint;
    public GameObject camera;
    void Start()
    {
        StartPoint = transform.position.x;
        startpointy = transform.position.y;
        Length = GetComponent<SpriteRenderer>().bounds.size.x; // 2;GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void FixedUpdate()
    {
        transform.position+=new Vector3(MovementSpeed,0,0)*Time.deltaTime;
        if(transform.position.x-camera.transform.position.x<=-Length)
        {
            transform.position = new Vector3(camera.transform.position.x,startpointy+camera.transform.position.y);
        }

    }
}
