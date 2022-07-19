using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]private ProgressManager ProgressManagerScript;

    [Header("Stamina Settings")]
    [SerializeField]private GameObject StaminaBar;
    [SerializeField]private float MaxStamina;
    [SerializeField]private float StaminaGainSpeed;
    #endregion

    void Start()
    {
        ProgressManagerScript.CurrentStamina = MaxStamina;
    }

    #region Main Stamina Functions
    void Update()
    {
        if (ProgressManagerScript.CurrentStamina == 0)
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
        if (ProgressManagerScript.CurrentStamina > MaxStamina)
        {
            ProgressManagerScript.CurrentStamina = MaxStamina;
        }
        if (ProgressManagerScript.CurrentStamina <= 0)
        {
            ProgressManagerScript.CurrentStamina = 0;
        }
    }
    private void StaminaRegain()
    {
        ProgressManagerScript.CurrentStamina += Time.deltaTime * StaminaGainSpeed;
    }
    private void StaminaBarControler()
    {
        StaminaBar.GetComponent<DisplaStamina>().MaxStamina = MaxStamina;
        StaminaBar.GetComponent<DisplaStamina>().CurrentStamina = ProgressManagerScript.CurrentStamina;
    }
    #endregion
    #region Animation Functions
    void StaminaFunc(float NeededStamina)
    {
        ProgressManagerScript.CurrentStamina -= NeededStamina;
    }
    #endregion
}
