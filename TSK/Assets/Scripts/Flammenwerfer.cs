using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammenwerfer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tree>())
            other.GetComponent<Tree>().Burn();
    }
}
