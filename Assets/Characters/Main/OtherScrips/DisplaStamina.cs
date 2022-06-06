using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaStamina : MonoBehaviour
{
    public Slider StaminaSlider;
    public float MaxStamina;
    public float CurrentStamina;
    void Start()
    {
        
    }

    void Update()
    {
        StaminaSlider.maxValue = MaxStamina;
        StaminaSlider.value = CurrentStamina;
    }
}
