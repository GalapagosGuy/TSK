using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelTypeController : MonoBehaviour
{

    [SerializeField]
    private Dropdown fuelDropdown;

    private MenuController.GroundAngleController ground;
    private Wildfire fire;
    void Start()
    {
        ground = FindObjectOfType<MenuController.GroundAngleController>();
        fire = FindObjectOfType<Wildfire>();
    }

    public void OnValueChanged()
    {
        if(fuelDropdown.value == 0)
        {
            // grass (short) Fine
            
            ground.surfaceToVolumeRatio = 3500f;
            ground.W_o = 0.034f;
            ground.fuelDepth = 1.0f;

        }

        if (fuelDropdown.value == 1)
        {
            // Chaparral Living
            ground.surfaceToVolumeRatio = 1500f;
            ground.W_o = 0.23f;          
            ground.fuelDepth = 6.0f;
        }

        if (fuelDropdown.value == 2)
        {
            // Timber (grass and understory) Living
            ground.surfaceToVolumeRatio = 1500f;
            ground.W_o = 0.023f;
            ground.fuelDepth = 1.5f;
        }

        if (fuelDropdown.value == 3)
        {
            // Logging slash (medium) Medium
            ground.surfaceToVolumeRatio = 109f;
            ground.W_o = 0.644f;
            ground.fuelDepth = 2.3f;
        }

        fire.Recalculate();
    }

}
