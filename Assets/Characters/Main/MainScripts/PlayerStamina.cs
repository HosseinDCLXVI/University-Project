using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public GameObject StaminaBar;

    public float MaxStamina = 100;
    public float StaminaGainSpeed;
    [HideInInspector]public float CurrentStamina;
    void Start()
    {
        CurrentStamina = MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentStamina < 100)
        {
            if (CurrentStamina == 0)
            {
                Invoke("StaminaGain", 2);
            }
            else
            {
                StaminaGain();
            }
        }
        else
        {
            CurrentStamina = 100;
        }
        if (CurrentStamina <= 0)
        {
            CurrentStamina = 0;
        }
        StaminaBarControl();
    }
    void StaminaGain()
    {
        CurrentStamina += Time.deltaTime * StaminaGainSpeed;
    }
    void StaminaBarControl()
    {
        StaminaBar.GetComponent<DisplaStamina>().MaxStamina = MaxStamina;
        StaminaBar.GetComponent<DisplaStamina>().CurrentStamina = CurrentStamina;
    }
    void StaminaFunc(float NeededStamina)
    {
        CurrentStamina -= NeededStamina;
    }
}
