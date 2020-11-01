using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildfire : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.0f;

    [SerializeField]
    private float maxXposition = 11.0f;

    [SerializeField]
    private float distanceToSpawnAnotherFire = 1.0f;
    private float currentDistanceReached = 0.0f;

    public GameObject wildFireParticles = null;

    public bool simulationStarted = false;

    private Vector3 startingPosition = Vector3.zero;
    private Vector3 oldPosition = Vector3.zero;

    private List<GameObject> spawnedFires = new List<GameObject>();

    private void Start()
    {
        startingPosition = this.transform.localPosition;
        oldPosition = startingPosition;
    }

    private void FixedUpdate()
    {
        if (!simulationStarted)
            return;

        if (this.transform.localPosition.x < maxXposition)
        {
            this.transform.localPosition += new Vector3(speed * Time.fixedDeltaTime, 0.0f, 0.0f);

            if (this.transform.localPosition.x > maxXposition)
                this.transform.localPosition = new Vector3(maxXposition, this.transform.localPosition.y, this.transform.localPosition.z);

            if (currentDistanceReached >= distanceToSpawnAnotherFire)
            {
                currentDistanceReached -= distanceToSpawnAnotherFire;
                SpawnFire();
            }

            currentDistanceReached += this.transform.localPosition.x - oldPosition.x;
            oldPosition = this.transform.localPosition;
        }
    }

    public void SpawnFire()
    {
        GameObject spawnedWildFire = Instantiate(wildFireParticles, this.transform.position, this.transform.rotation);
        spawnedFires.Add(spawnedWildFire);
    }

    public void Reset()
    {
        this.transform.localPosition = startingPosition;
        oldPosition = startingPosition;

        foreach (GameObject go in spawnedFires)
            Destroy(go);

        spawnedFires.Clear();
    }
}
