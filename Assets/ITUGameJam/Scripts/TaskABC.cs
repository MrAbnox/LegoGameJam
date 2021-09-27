using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskABC : MonoBehaviour
{
    [SerializeField] private float maxActiveTime;
    [SerializeField] private float timeToFinishTask;
    [SerializeField] private Image slider;

    [SerializeField] private ScreenShake screenShake;

    private bool isActive;
    private bool isPlayerInside;
    private float currentTime;
    private bool isCoroutineRunning;

    private void Start()
    {
        isCoroutineRunning = false;
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
        Debug.Log("ACTIVATE ABC TASK");
        isActive = true;
    }

    public void Destroy()
    {
        isActive = false;
        //TODO: Use bricks function to destroy blocks nicely
    }

    private void Update()
    {
        if(isActive)
        {
            if (!isCoroutineRunning)
            {
                Debug.Log("COROUTINE RUNNING");
                StartCoroutine(WaitToStopTask());
                isCoroutineRunning = true;
            }

            if(isPlayerInside)
                {

                currentTime += Time.deltaTime;
                slider.fillAmount = currentTime/timeToFinishTask;
                if (currentTime >= timeToFinishTask)
                {
                    slider.transform.parent.gameObject.SetActive(false);
  
                    currentTime = 0;
                    isActive = false;
                    GameManager.instance.IsABCTaskActive = false;
                    //TODO: Do some effect when this is done
                }
            }
        }
    }

    IEnumerator WaitToStopTask()
    {
        yield return new WaitForSeconds(maxActiveTime);

        isCoroutineRunning = false;

        if (isActive)
        {
            Debug.Log("STOP TASK");
            isActive = false;
            currentTime = 0;
            GameManager.instance.IsABCTaskActive = false;
            slider.transform.parent.gameObject.SetActive(false);

            screenShake.shakeDuration = 2;

            //TODO: Mask Task failing visual
        }
    }
}
