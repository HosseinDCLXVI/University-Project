using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private Transform HeadTransform;
    [SerializeField] private Transform TailTransform;
    [SerializeField] private Vector2 headforce;
    [SerializeField] private Vector2 tailforce;
     public Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        rigidbody.AddForceAtPosition(headforce, HeadTransform.position*Time.fixedDeltaTime);
        rigidbody.AddForceAtPosition(tailforce, TailTransform.position*Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Ground")
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
            Debug.Log("cooool");
        }
        else if(collision.tag=="Wall")
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
        }
        else if(collision.tag =="Enemy")
        {
            Destroy(this.gameObject);
        }
    }

}
