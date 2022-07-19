using UnityEngine;
using UnityEngine.UI;

namespace GMTK2022
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] Health healthToDisplay;
        Slider hSlider;

        private void Start() {
            if(healthToDisplay) {
                hSlider = GetComponent<Slider>();
                hSlider.maxValue = healthToDisplay.GetMaxHealth();
                hSlider.value = healthToDisplay.GetCurrentHealth();
            }
        }
        private void Update() {
            if(healthToDisplay) {
                hSlider.value = healthToDisplay.GetCurrentHealth();
            }

        }

    }
}
