using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private bool isBurining;

    public Color leavesColor = Color.green;

    private Color currentColor;

    [SerializeField]
    private float burningSpeed = 1.0f;

    public MeshRenderer[] leavesRenderers;

    private void Start()
    {
        currentColor = leavesColor;
    }

    public void Burn()
    {
        isBurining = true;
    }

    private void Update()
    {
        if (isBurining)
        {
            float r = currentColor.r - burningSpeed * Time.deltaTime;
            if (r < 0)
                r = 0;
            float g = currentColor.g - burningSpeed * Time.deltaTime;
            if (g < 0)
                g = 0;
            float b = currentColor.b - burningSpeed * Time.deltaTime;
            if (b < 0)
                b = 0;

            currentColor = new Color(r, g, b);

            foreach (MeshRenderer mr in leavesRenderers)
            {
                mr.material.color = currentColor;
            }
        }
    }

    public void Reset()
    {
        isBurining = false;
        currentColor = leavesColor;

        foreach (MeshRenderer mr in leavesRenderers)
        {
            mr.material.color = currentColor;
        }
    }
}
