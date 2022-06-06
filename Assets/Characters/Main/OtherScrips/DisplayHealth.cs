using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public Slider HealthSlider;
    public float MaxHealth;
    public float CurrentHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = CurrentHealth;      
    }
}
