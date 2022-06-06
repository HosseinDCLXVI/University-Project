using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    public GameObject Player;
    public Animator MainAnimator;
    public GameObject HealthBar;
    public GameObject GameOverCanvas;
    public Volume BlurEffect;
    public float MaxHealth = 100;
    [HideInInspector]public float CurrentHealth;
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarControll();
        if (CurrentHealth <= 0)
        {
            MainAnimator.SetBool("Die", true);
            DieFunc();
        }
    }

    void HealthBarControll()
    {
        HealthBar.GetComponent<DisplayHealth>().MaxHealth = MaxHealth;
        HealthBar.GetComponent<DisplayHealth>().CurrentHealth = CurrentHealth;
    }

    public void PlayerDamage(int damage)
    {
        CurrentHealth -= damage;
        MainAnimator.SetTrigger("Hit");

    }
    public void DieFunc()
    {
        Destroy(Player, 3);
        Invoke("GameOver", 1);
    }
    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
        BlurEffect.weight = 1f;
        Time.timeScale = 0;
    }

}
