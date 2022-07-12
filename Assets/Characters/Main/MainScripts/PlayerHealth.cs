using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    #region Inspector Variables
    [Header("Health Settings")]
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private float MaxHealth;
    #endregion

    private Animator MainAnimator;

    [HideInInspector]public Volume BlurEffect;
    [HideInInspector]public float CurrentHealth;

    void Start()
    {
        MainAnimator=GetComponent<Animator>();
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        HealthBarControll();
        DeathFunc();
    }

    void HealthBarControll()
    {
        HealthBar.GetComponent<DisplayHealth>().MaxHealth = MaxHealth;
        HealthBar.GetComponent<DisplayHealth>().CurrentHealth = CurrentHealth;
    }

    public void DeathFunc()
    {
        if (CurrentHealth <= 0)
        {
            MainAnimator.SetBool("Die", true);
            Destroy(this, 3);
            Invoke("GameOver", 1);
        }

    }
    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
        BlurEffect.weight = 1f;
        Time.timeScale = 0;
    }

    public void PlayerDamage(int damage) //gets called from enemy attack script
    {
        CurrentHealth -= damage;
        MainAnimator.SetTrigger("Hit");

    }

}
