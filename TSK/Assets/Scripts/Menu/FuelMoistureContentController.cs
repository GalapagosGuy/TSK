using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MenuController
{
    public class FuelMoistureContentController : MonoBehaviour
    {
        public Slider slider = null;
        public TextMeshProUGUI valueDisplay = null;
        public MeshRenderer groundMeshRenderer = null;

        [SerializeField]
        private Color minValueColor = Color.green;

        [SerializeField]
        private Color maxValueColor = Color.green;

        private Wildfire fire;
        private MenuController.GroundAngleController ground;

        private void Start()
        {
            fire = FindObjectOfType<Wildfire>();
            ground = FindObjectOfType<MenuController.GroundAngleController>();
        }

        public void OnValueChanged()
        {
            if (slider)
            {
                if (valueDisplay)
                    valueDisplay.text = (Mathf.Round(slider.value * 10000) / 10000) + "";

                ground.M_f = slider.value;
                fire.Recalculate();

                if (groundMeshRenderer)
                {
                    float r = minValueColor.r + ((maxValueColor.r - minValueColor.r) * (slider.value / slider.maxValue));
                    float g = minValueColor.g + ((maxValueColor.g - minValueColor.g) * (slider.value / slider.maxValue));
                    float b = minValueColor.b + ((maxValueColor.b - minValueColor.b) * (slider.value / slider.maxValue));
                    groundMeshRenderer.sharedMaterial.color = new Color(r, g, b);
                }
            }
        }
    }
}
