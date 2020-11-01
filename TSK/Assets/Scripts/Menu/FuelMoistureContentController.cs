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

        public void OnValueChanged()
        {
            if (slider)
            {
                if (valueDisplay)
                    valueDisplay.text = (Mathf.Round(slider.value * 10000) / 10000) + "";

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
