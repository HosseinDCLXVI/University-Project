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

    private AudioSource ArrowAudioSource;
    [SerializeField] private AudioClip[]Arrow ;
    [SerializeField] private AudioClip[]ArrowHit ;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ArrowAudioSource=GetComponent<AudioSource>();

        int x=Random.Range(0,Arrow.Length);
        ArrowAudioSource.clip=Arrow[x];
        ArrowAudioSource.Play();

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
            int x = Random.Range(0, ArrowHit.Length);
            ArrowAudioSource.clip = ArrowHit[x];
            ArrowAudioSource.Play();
        }
        else if(collision.tag=="Wall")
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
            int x = Random.Range(0, ArrowHit.Length);
            ArrowAudioSource.clip = Arrow[x];
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
            collision.GetComponentInParent<EnemyController>().EnemyIsAwareOfThePlayer = true;
            if (collision.GetComponentInParent<EnemyPatrol>().PlayerShouldStayUndetected)
            {
                PlayerHealthScript.InvokeGameOver(3);
            }
        }
    }
}
