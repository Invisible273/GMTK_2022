using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Health healthToDisplay;
    Slider hSlider;

    private void Start() 
    {
        hSlider = GetComponent<Slider>();
        hSlider.maxValue = healthToDisplay.GetMaxHealth();
        hSlider.value = healthToDisplay.GetCurrentHealth();
    }
    private void Update() {
        if(healthToDisplay)
        {
            hSlider.value = healthToDisplay.GetCurrentHealth();
        }
        
    }

}
