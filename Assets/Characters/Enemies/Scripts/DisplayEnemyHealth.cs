using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyHealth : MonoBehaviour
{
    public Slider HealthSlider;
    public float MaxHealth;
    public float CurrentHealth;
    void Start()
    {
        
    }

    void Update()
    {
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = CurrentHealth;
    }
}
