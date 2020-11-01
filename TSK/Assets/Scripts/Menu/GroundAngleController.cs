using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MenuController
{
    public class GroundAngleController : MonoBehaviour
    {
        public Slider slider = null;
        public TextMeshProUGUI valueDisplay = null;
        public GameObject ground = null;

        public void OnValueChanged()
        {
            if (slider)
            {
                if (valueDisplay)
                    valueDisplay.text = slider.value + "";

                if (ground)
                    ground.transform.rotation = Quaternion.Euler(0, 0, slider.value);
            }
        }
    }
}
