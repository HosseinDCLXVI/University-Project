using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] private ProgressManager ProgressManagerScript;
    [Header("Health Settings")]
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private float MaxHealth;
    #endregion

    private Animator MainAnimator;

    void Start()
    {
        MainAnimator=GetComponent<Animator>();
        ProgressManagerScript.CurrentHealth = MaxHealth;
    }

    void Update()
    {
        HealthBarController();
        DeathFunc();
    }


    void HealthBarController()
    {
        HealthBar.GetComponent<DisplayHealth>().MaxHealth = MaxHealth;
        HealthBar.GetComponent<DisplayHealth>().CurrentHealth = ProgressManagerScript.CurrentHealth;
    }

    public void DeathFunc()
    {
        if (ProgressManagerScript.CurrentHealth <= 0)
        {
            MainAnimator.SetBool("Die", true);
            Destroy(this, 3);
            Invoke("GameOver", 3);
        }

    }
    public void InvokeGameOver(float InvokeTime=0)
    {
        Invoke("GameOver", 3);
    }
    void GameOver()
    {
        GameOverCanvas.SetActive(true);
        ProgressManagerScript.BlurEffect.weight = 1f;
        Time.timeScale = 0;
    }

    public void PlayerDamage(int damage) //gets called from enemy attack script
    {
        ProgressManagerScript.CurrentHealth -= damage;
        MainAnimator.SetTrigger("Hit");
    }

}