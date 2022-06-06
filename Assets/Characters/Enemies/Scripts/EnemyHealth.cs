using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator EnemyAnimator;
    public GameObject EnemyPrefab;
    public GameObject Etop;
    public GameObject EnemyHealthBar;
    public GameObject HealthBarCanvas;
    public float EnemyMaxHealth = 100;
    public float EnemyCurrentHealth;
    void Start()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
    }

    void FixedUpdate()
    {
        EnemyHealthBar.GetComponent<DisplayEnemyHealth>().MaxHealth = EnemyMaxHealth;
        EnemyHealthBar.GetComponent<DisplayEnemyHealth>().CurrentHealth = EnemyCurrentHealth;
    }
    public void EnemyDamage(float damage)
    {
        EnemyCurrentHealth -= damage;
        EnemyAnimator.SetTrigger("Hit");
        if (EnemyCurrentHealth <= 0)
        {
            EnemyAnimator.SetBool("Die", true);
            DieFunc();
        }
    }
    void DieFunc()
    {
        EnemyAnimator.SetBool("BackToLife", false);
        EnemyHealthBar.SetActive(false);
        EnemyPrefab.GetComponent<EnemyPatrol>().enabled = false;
        Destroy(EnemyPrefab, 2);
        Etop.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void RemoveCanvas()
    {
        HealthBarCanvas.SetActive(false);
    }
   public void AddCanvas()
    {
        HealthBarCanvas.SetActive(true);
    }


}

