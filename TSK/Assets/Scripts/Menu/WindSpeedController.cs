using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MenuController
{
    public class WindSpeedController : MonoBehaviour
    {
        public Slider slider = null;
        public TextMeshProUGUI valueDisplay = null;
        public ParticleSystem wind = null;

        [SerializeField]
        private float windParticleMinSpeed = 1.0f;

        [SerializeField]
        private float windParticleMaxSpeed = 5.0f;

        public void OnValueChanged()
        {
            if (slider)
            {
                if (valueDisplay)
                    valueDisplay.text = slider.value + "";

                if (wind)
                {
                    wind.gameObject.SetActive(slider.value > slider.minValue);

                    var windMain = wind.main;

                    if (slider.value == slider.minValue)
                        windMain.simulationSpeed = 0.0f;
                    else
                    {
                        windMain.simulationSpeed = windParticleMinSpeed + ((windParticleMaxSpeed - windParticleMinSpeed) * (slider.value / slider.maxValue));
                    }
                }
            }
        }
    }
}
