using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyController EnemyControllerScript;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject Etop;
    [SerializeField] private GameObject EnemyHealthBar;
    [SerializeField] private GameObject HealthBarCanvas;
    [SerializeField] private float EnemyMaxHealth;
    private Animator EnemyAnimator;
    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        EnemyControllerScript.EnemyCurrentHealth = EnemyMaxHealth;
        EnemyControllerScript.EnemyMaxHealth = EnemyMaxHealth;
    }
    private void Update()
    {
    }

    void FixedUpdate()
    {
        EnemyHealthBar.GetComponent<DisplayEnemyHealth>().MaxHealth = EnemyMaxHealth;
        EnemyHealthBar.GetComponent<DisplayEnemyHealth>().CurrentHealth = EnemyControllerScript.EnemyCurrentHealth;
    }
    public void EnemyDamage(float damage)
    {
        EnemyControllerScript.EnemyCurrentHealth -= damage;
        EnemyAnimator.SetTrigger("Hit");
        if (EnemyControllerScript.EnemyCurrentHealth <= 0)
        {
            EnemyAnimator.SetBool("Die", true);
            Rigidbody2D rigidbody2D =GetComponent<Rigidbody2D>();
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
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

