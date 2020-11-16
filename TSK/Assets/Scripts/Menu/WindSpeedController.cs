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

        // PARAMETRY PODŁOŻA
        [Range(0, 5468)]
        public float U;  // prędkość wiatru w środkowym punkcie pożaru - 0 - 5468 ft/min

        // ZMIENNE POMOCNICZE
        [HideInInspector]
        public float C, B, E;

        private MenuController.GroundAngleController ground;
        private Wildfire fire;

        private void Start()
        {
            ground = FindObjectOfType<MenuController.GroundAngleController>();
            fire = FindObjectOfType<Wildfire>();
           
        }
        public void CalculateSecondaryVariables()
        {
            // WZORY Z OBIEKTU WIATR
            C = 7.47f * Mathf.Exp(-0.133f * Mathf.Pow(ground.surfaceToVolumeRatio, 0.55f));
            B = 0.0256f * Mathf.Pow(ground.surfaceToVolumeRatio, 0.54f); // questionably wzór B - możliwe że powinno być fuelDepth * 0.54;
            E = 0.715f * Mathf.Exp(-3.59f * Mathf.Pow(10f, -4f) * ground.surfaceToVolumeRatio);
        }
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

                U = slider.value;
                fire.Recalculate();
            }
        }
    }
}
