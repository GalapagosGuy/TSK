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

        private Wildfire fire;

        // PARAMETRY PODŁOŻA
        public float groundAngle; // Kąt nachylenia podłoża - 0.0 - 60.0
        public float M_f; // Zawartość wilgoci w paliwie (fuel particle moisture content)
        public float W_o;  // Nadmierna ładowność paliwa (ovendry fuel loading) - WARTOŚĆ STAŁA Z TABELI
        public float fuelDepth; // WARTOŚĆ STAŁA Z TABELI
        public float surfaceToVolumeRatio; // WARTOŚĆ STAŁA Z TABELI

        // STAŁE PODŁOŻA - UPROSZCZENIA
        public const float ro_p = 32.0f;  // nadmierna gęstość cząstek posiada stałą wartość 32.0 (lb/ft.^3)
        public const float h = 8000.0f; // cząstki paliwa o niskiej zawartości ciepła
        public const float M_x = 0.3f; // wilgotność wyginięcia (moisture content of extintion)
        public const float S_t = 0.0555f; // całkowita zawartość składników mineralnych cząstek paliwa (fuel particle total mineral content)
        public const float S_e = 0.010f; // efektywna zawartość minerałów cząsteczek paliwa(fuel particle effective mineral content)

        private void Start()
        {
            fire = FindObjectOfType<Wildfire>();
            valueDisplay.text = slider.value.ToString();
            groundAngle = slider.value;
        }

        public void OnValueChanged()
        {
            if (slider)
            {
                groundAngle = slider.value;
                fire.Recalculate();

                if (valueDisplay)
                    valueDisplay.text = slider.value + "";

                if (ground)
                    ground.transform.rotation = Quaternion.Euler(0, 0, slider.value);
            }
        }
    }
}
