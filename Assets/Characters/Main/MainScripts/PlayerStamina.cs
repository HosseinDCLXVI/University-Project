using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    #region Inspector Variables
    [Header("Stamina Settings")]
    [SerializeField]private GameObject StaminaBar;
    [SerializeField]private float MaxStamina;
    [SerializeField]private float StaminaGainSpeed;
    #endregion

    [HideInInspector]public float CurrentStamina;
    void Start()
    {
        CurrentStamina = MaxStamina;
    }

    #region Main Stamina Functions
    void Update()
    {
        if (CurrentStamina == 0)
        {
            Invoke("StaminaGain", 2);
        }
        else
        {
            StaminaRegain();
        }

        StaminaNormalization();
        StaminaBarControler();
    }
    private void StaminaNormalization() // Chech if stamina has the right amount 
    {
        if (CurrentStamina > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }
        if (CurrentStamina <= 0)
        {
            CurrentStamina = 0;
        }
    }
    private void StaminaRegain()
    {
        CurrentStamina += Time.deltaTime * StaminaGainSpeed;
    }
    private void StaminaBarControler()
    {
        StaminaBar.GetComponent<DisplaStamina>().MaxStamina = MaxStamina;
        StaminaBar.GetComponent<DisplaStamina>().CurrentStamina = CurrentStamina;
    }
    #endregion
    #region Animation Functions
    void StaminaFunc(float NeededStamina)
    {
        CurrentStamina -= NeededStamina;
    }
    #endregion
}
