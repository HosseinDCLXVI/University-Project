using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject Camera;
    public float StartPoint, ParallaxEffect , Length;
    void Start()
    {
        StartPoint =transform.position.x;
        Length=GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = Camera.transform.position.x * (1 - ParallaxEffect);
        float Distance = Camera.transform.position.x * ParallaxEffect;


        transform.position =new Vector3 (StartPoint + Distance,transform.position.y,transform.position.z);

        if(temp>Length+StartPoint)
        {
            StartPoint += Length;
        }
        else if(temp<StartPoint-Length)
        {
            StartPoint -= Length;
        }
    }
}
