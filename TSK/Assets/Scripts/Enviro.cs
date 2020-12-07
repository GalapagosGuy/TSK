using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviro : MonoBehaviour
{
    public GameObject[] grass;
    public GameObject[] chaparral;
    public GameObject[] timber;
    public GameObject[] loggingSlash;

    public void ShowGrass()
    {
        DisplayModels(grass, true);
        DisplayModels(chaparral, false);
        DisplayModels(timber, false);
        DisplayModels(loggingSlash, false);
    }

    public void ShowChaparral()
    {
        DisplayModels(grass, false);
        DisplayModels(chaparral, true);
        DisplayModels(timber, false);
        DisplayModels(loggingSlash, false);
    }

    public void ShowTimber()
    {
        DisplayModels(grass, false);
        DisplayModels(chaparral, false);
        DisplayModels(timber, true);
        DisplayModels(loggingSlash, false);
    }

    public void ShowLoggingSlash()
    {
        DisplayModels(grass, false);
        DisplayModels(chaparral, false);
        DisplayModels(timber, false);
        DisplayModels(loggingSlash, true);
    }

    private void DisplayModels(GameObject[] modelsPack, bool value)
    {
        foreach (GameObject go in modelsPack)
            go.SetActive(value);
    }
}
