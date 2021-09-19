using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    // Start is called before the first frame update

    public RobotController rc; // direct reference to the controller sourcing variables
    [SerializeField] private float lightSpeed;
    public Light[] lights; // array of light elements to be adjusted.
    private float timer; // timer/intensity of lights.
    private const float timerCap = 500.0f; // constant value that does not change

    private float lastKnownPosition; // motor: helps in figuring out if there is growth in position and translate that to timer time.
    public float currentPosition; // motor: keeps track of the motor position

    void Start()
    {
        timer = 500.0f; //debug value
        lastKnownPosition = rc.currentRollValue;
    }

    // Update is called once per frame
    void Update()
    {
        //needs and update to robotController.
        currentPosition = rc.currentPitchValue;
        Debug.Log("RC1 " + rc.currentPitchValue);
        // If motor position is in growth, add the growth to the timer.
        if (currentPosition > lastKnownPosition)
        {
            timer = timer + (currentPosition - lastKnownPosition);
            CheckTimer();
        }
        lastKnownPosition = currentPosition;
        UpdateLights();
        timer -= lightSpeed * Time.deltaTime;

    }

    // Updates individual lights with the timer as intensity, gradually decreasing them.
    private void UpdateLights()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = timer;
        }
    }

    private void CheckTimer()
    {
        if (timer >= 500.0f)
        {
            timer = 500.0f;
        }

    }
}
