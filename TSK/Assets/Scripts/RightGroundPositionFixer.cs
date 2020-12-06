using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightGroundPositionFixer : MonoBehaviour
{
    public GameObject objectToFollow;

    private void Update()
    {
        this.transform.position = objectToFollow.transform.position;
    }
}
