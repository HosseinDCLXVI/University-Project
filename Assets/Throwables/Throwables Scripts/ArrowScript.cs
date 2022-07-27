using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    [HideInInspector]public Rigidbody2D rigidbody;
    [HideInInspector] public PlayerHealth PlayerHealthScript;
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
            SeeIfEnemyShoulKnowAboutPlayer(collision);
        }
        else if (collision.tag == "Body")
        {
            collision.GetComponentInParent<EnemyHealth>().EnemyDamage(BodyShotDamage);
            this.gameObject.SetActive(false);
            SeeIfEnemyShoulKnowAboutPlayer(collision);

        }
        else if (collision.tag == "Feet")
        {
            collision.GetComponentInParent<EnemyHealth>().EnemyDamage(BodyShotDamage);
            this.gameObject.SetActive(false);
            SeeIfEnemyShoulKnowAboutPlayer(collision);

        }

    }
    void SeeIfEnemyShoulKnowAboutPlayer(Collider2D collision)
    {
        if (collision.GetComponentInParent<EnemyController>().EnemyCurrentHealth > 0)
        {
            Debug.Log(collision.GetComponentInParent<EnemyController>().EnemyCurrentHealth);
            collision.GetComponentInParent<EnemyController>().EnemyIsAwareOfThePlayer = true;
            if (collision.GetComponentInParent<EnemyPatrol>().PlayerShouldStayUndetected)
            {
                PlayerHealthScript.InvokeGameOver(3);
            }
        }
    }
}
