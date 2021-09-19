using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskABC : MonoBehaviour
{

    [SerializeField] private RobotController robotController;
    [SerializeField] private float maxActiveTime;
    [SerializeField] private float timeToFinishTask;
    [SerializeField] private Image slider;

    private bool isActive;
    private bool isPlayerInside;
    private float currentTime;

    private void Start()
    {
        slider.fillAmount = 0;
        slider.transform.parent.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (isActive)
                slider.transform.parent.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            slider.transform.parent.gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Destroy()
    {
        isActive = false;
        //TODO: Use bricks function to destroy blocks nicely
    }

    private void Update()
    {
        if(isActive && isPlayerInside)
        {
            currentTime += Time.deltaTime;
            slider.fillAmount = currentTime/timeToFinishTask;
            if (currentTime >= timeToFinishTask)
            {
                slider.transform.parent.gameObject.SetActive(false);
  
                currentTime = 0;
                isActive = false;
                robotController.isABCTaskActive = false;
                //TODO: Do some effect when this is done
            }
        }
    }
}
