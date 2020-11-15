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

    private MenuController.WindSpeedController wind;
    private MenuController.GroundAngleController ground;

    // ZMIENNE GŁÓWNEGO WZORU
    private float R;
    private float delta_w;
    private float delta_s;


    // ZMIENNE POMOCNICZE GŁÓWNEGO WZORU
    private float Beta;
    private float Beta_op;
    private float ro_b;
    private float Q_ig;
    private float W_n;
    private float A;
    private float e;
    private float Epsilon;

    private void Start()
    {
        // REFERENCJE OBIEKTÓW
        wind = FindObjectOfType<MenuController.WindSpeedController>();
        ground = FindObjectOfType<MenuController.GroundAngleController>();

        Beta_op = Mathf.Pow(3.348f * ground.surfaceToVolumeRatio, -0.8189f);
        ro_b = ground.W_o / ground.fuelDepth;
        Q_ig = 250f + 1.116f * ground.M_f;
        W_n = ground.W_o / (1 + MenuController.GroundAngleController.S_t);
        A = 1f / (Mathf.Pow(4.774f * ground.surfaceToVolumeRatio, 0.1f) - 7.27f);
        e = Mathf.Exp(-138 / ground.surfaceToVolumeRatio);


        // wzory z głównego wzoru
        delta_w = wind.C * Mathf.Pow(wind.U, wind.B) * Mathf.Pow((Beta/Beta_op), -1f * wind.E);
        delta_s = Mathf.Pow((5.275f * Beta), -0.3f) * Mathf.Pow((Mathf.Tan(ground.groundAngle)), 2f);


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
