using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLighting : MonoBehaviour
{
    [SerializeField] private float speedDecrease;
    [SerializeField] private float darknessLimit;
    private Light light;

    private float maxLight;
    private void Start()
    {
        light = GetComponent<Light>();
        maxLight = 1.0f;
    }

    private void FixedUpdate()
    {
        float temp = light.color.r - speedDecrease * Time.deltaTime;
        if (temp <= darknessLimit)
        {
            temp = darknessLimit;
            Debug.Log(Time.realtimeSinceStartup);
        }

        Color color = new Color(temp, temp, temp);

        light.color = color;
    }
}
