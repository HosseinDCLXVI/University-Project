using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

     [HideInInspector]public Rigidbody2D rigidbody;
    [SerializeField] private int HeadShotDamage;
    [SerializeField] private int BodyShotDamage;
    [SerializeField] private int FeetShotDamage;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody.rotation=Mathf.Atan2(rigidbody.velocity.y,rigidbody.velocity.x)*Mathf.Rad2Deg;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Ground")
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
        }
        else if(collision.tag=="Wall")
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
        }
        else if(collision.tag =="Head")
        {
            collision.GetComponentInParent<EnemyHealth>().EnemyDamage(HeadShotDamage);
            this.gameObject.SetActive(false);

        }
        else if (collision.tag == "Body")
        {
            collision.GetComponentInParent<EnemyHealth>().EnemyDamage(BodyShotDamage);
            this.gameObject.SetActive(false);

        }
        else if (collision.tag == "Feet")
        {
            collision.GetComponentInParent<EnemyHealth>().EnemyDamage(BodyShotDamage);
            this.gameObject.SetActive(false);

        }
    }

}
