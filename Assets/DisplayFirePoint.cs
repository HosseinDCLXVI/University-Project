using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFirePoint : MonoBehaviour
{
    [SerializeField] private Slider StaminaSlider;


    [HideInInspector] public float MaxFirePoint;
    [HideInInspector] public float CurrentFirePoint;
    [SerializeField] GameObject FullBarLogo;

    void Update()
    {
        StaminaSlider.maxValue = MaxFirePoint;
        StaminaSlider.value = CurrentFirePoint;

        if(StaminaSlider.value==StaminaSlider.maxValue)
        {
            FullBarLogo.SetActive(true);
        }
        else
        {
            FullBarLogo.SetActive(false);
        }
    }
}
