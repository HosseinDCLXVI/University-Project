using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public Vector2 ZoneSize;
    public GameObject Camera;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            collision.GetComponent<PlayerHealth>().InvokeGameOver();
        }
    }
}
