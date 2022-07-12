using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaStamina : MonoBehaviour
{
    [SerializeField]private Slider StaminaSlider;

    [HideInInspector]public float MaxStamina;
    [HideInInspector]public float CurrentStamina;

    void Update()
    {
        StaminaSlider.maxValue = MaxStamina;
        StaminaSlider.value = CurrentStamina;
    }
}
