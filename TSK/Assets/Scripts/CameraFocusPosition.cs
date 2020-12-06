using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusPosition : MonoBehaviour
{
    public GameObject objectToFollow = null;

    [SerializeField]
    private float positionOffsetZ = -4.0f;
    [SerializeField]
    private float positionOffsetY = 3.5f;
    [SerializeField]
    private float maxAngle = 60.0f;
    [SerializeField]
    private float baseZposition = -14.0f;
    [SerializeField]
    private float baseYposition = 2.5f;

    private void FixedUpdate()
    {
        if (objectToFollow)
        {
            float percentageValue = objectToFollow.transform.rotation.eulerAngles.z / maxAngle;

            Vector3 position = this.transform.position;
            position.z = baseZposition + percentageValue * positionOffsetZ;
            position.y = baseYposition + percentageValue * positionOffsetY;
            this.transform.position = position;
        }
    }
}
