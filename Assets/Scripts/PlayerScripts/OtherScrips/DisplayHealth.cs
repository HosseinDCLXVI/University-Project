using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField]private Slider HealthSlider;

    [HideInInspector]public float MaxHealth;
    [HideInInspector]public float CurrentHealth;

    void Update()
    {
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = CurrentHealth;      
    }
}
