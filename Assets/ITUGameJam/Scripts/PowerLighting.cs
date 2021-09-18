using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLighting : MonoBehaviour
{
    [SerializeField] private float speedDecrease;
    [SerializeField] private float darknessLimit;

    [SerializeField] private RobotController robotController;
    [SerializeField] Light[] lights;

    [Header("Battery/Wheel")]
    [SerializeField] float chargeSpeed;
    [SerializeField] float batteryCharge;

    private float engineAngle;
    private bool isBatteryTask;
    private float startingLight;

    private float maxLight;
    private void Start()
    {
        engineAngle = robotController.currentPitchValue;
        maxLight = 1.0f;
    }

    private void Update()
    {
        engineAngle = robotController.currentPitchValue;
    }

    private void FixedUpdate()
    {
        float temp = RenderSettings.ambientLight.r - speedDecrease * Time.deltaTime;
        if (temp <= darknessLimit)
        {
            temp = darknessLimit;
            //Debug.Log(Time.realtimeSinceStartup);
        }

        Color color = new Color(temp, temp, temp);

        RenderSettings.ambientLight= color;
    }

    // private void LateUpdate()
    // {
    //     if (batteryCharge > 0 && !isBatteryTask)
    //     {
    //         Debug.Log("TEST: " + engineAngle);
    //         Debug.Log("TEST2: " + robotController.currentPitchValue);
    //         if (engineAngle != robotController.currentPitchValue)
    //         {
    //
    //             float temp = RenderSettings.ambientLight.r + robotController.currentPitchValue;
    //
    //             Color tempColor = new Color(temp, temp, temp);
    //             RenderSettings.ambientLight = tempColor;
    //         }
    //     }
    //
    //     RenderSettings.ambientIntensity = 1.0f;
    // }
}
