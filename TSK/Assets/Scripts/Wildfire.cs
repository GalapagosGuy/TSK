using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI resultKmHText;
    public TextMeshProUGUI resultmsText;
    public GameObject wildFireParticles = null;
    public GameObject groundObject = null;

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
    private float I_R;
    private float Epsilon;
    private float Gamma;
    private float n_m;
    private float n_s;

    // ZMIENNE POMOCNICZE GŁÓWNEGO WZORU
    private float Beta;
    private float Beta_op;
    private float ro_b;
    private float Q_ig;
    private float W_n;
    private float A;
    private float e;
    private float Gamma_max;

    private float nominator, denominator;

    private void Start()
    {
        Calculate();
        startingPosition = this.transform.localPosition;
        oldPosition = startingPosition;
    }

    private void Calculate()
    {
        // REFERENCJE OBIEKTÓW
        wind = FindObjectOfType<MenuController.WindSpeedController>();
        ground = FindObjectOfType<MenuController.GroundAngleController>();

        // OBLICZENIE POBOCZNYCH WZORÓW
        wind.CalculateSecondaryVariables();


        Beta_op = 3.348f * Mathf.Pow(ground.surfaceToVolumeRatio, -0.8189f);
        ro_b = (float)ground.W_o / ground.fuelDepth;
        //Beta = (float)ro_b / MenuController.GroundAngleController.ro_p;
        //Beta = 0.036f;
        Beta = 0.07f;
        Q_ig = 250f + 1116f * ground.M_f;
        W_n = ground.W_o * (1f - MenuController.GroundAngleController.S_t);
        A = 133 * Mathf.Pow(ground.surfaceToVolumeRatio, -0.7913f);//1f / ((4.774f * Mathf.Pow(ground.surfaceToVolumeRatio, 0.1f)) - 7.27f);
        e = Mathf.Exp(-138f / ground.surfaceToVolumeRatio);


        // WZORY Z GŁÓWNEGO WZORU
        Epsilon = Mathf.Pow((192f + 0.2595f * ground.surfaceToVolumeRatio), -1f) * Mathf.Exp((0.792f + 0.681f * Mathf.Pow(ground.surfaceToVolumeRatio, 0.5f)) * (Beta + 0.1f));
        delta_w = wind.C * Mathf.Pow(wind.U, wind.B) * Mathf.Pow((Beta / Beta_op), -1f * wind.E);
        delta_s = 5.275f * Mathf.Pow(Beta, -0.3f) * Mathf.Pow((Mathf.Tan(Mathf.Deg2Rad * ground.groundAngle)), 2f);
        Gamma_max = Mathf.Pow(ground.surfaceToVolumeRatio, 1.5f) * Mathf.Pow(495f + 0.0594f * Mathf.Pow(ground.surfaceToVolumeRatio, 1.5f), -1f);
        Gamma = Gamma_max * Mathf.Pow((Beta / Beta_op), A) * Mathf.Exp(A * (1 - (Beta / Beta_op)));

        float rM = ground.M_f / MenuController.GroundAngleController.M_x;
        if (rM > 1.0f)
            rM = 1.0f;

        n_m = 1f - 2.59f * (rM) + 5.11f * Mathf.Pow((rM), 2) - 3.52f * Mathf.Pow((rM), 3);
        n_s = 0.174f * Mathf.Pow(MenuController.GroundAngleController.S_e, -0.19f);
        if (n_s > 1.0f)
            n_s = 1.0f;

        I_R = Gamma * W_n * MenuController.GroundAngleController.h * n_m * n_s;

        nominator = I_R * Epsilon * (1f + delta_w + delta_s);
        denominator = ro_b * e * Q_ig;

        // FINAL RESULT
        R = nominator / denominator;
        float Rkmh = R * 0.0003048f * 60f;
        float Rms = R * 0.3048f / 60f;

        resultText.text = "R = " + R;
        resultText.color = Color.Lerp(Color.white, Color.red, R / 2300f );

        resultKmHText.text = "R = " + Rkmh;
        resultKmHText.color = Color.Lerp(Color.white, Color.red, R / 2300f);

        resultmsText.text = "R = " + Rms;
        resultmsText.color = Color.Lerp(Color.white, Color.red, R / 2300f);
    }

    public void Recalculate()
    {
        Calculate();
    }

    private void FixedUpdate()
    {
        if (!simulationStarted)
            return;

        if (this.transform.localPosition.x < maxXposition)
        {
            this.transform.localPosition += new Vector3(R * Time.fixedDeltaTime / 10 / 60, 0.0f, 0.0f);

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
        spawnedWildFire.transform.SetParent(groundObject.transform);
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
