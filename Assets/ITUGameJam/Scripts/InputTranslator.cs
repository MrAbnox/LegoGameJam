using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTranslator : MonoBehaviour
{
    //public Canvas canvas;
    public Image topBar;
    public Image botBar;
    public Image line;

    public GameObject onOff; // Light Element for now.
    private bool isOn; //is Activity in play?
    private bool isActive; //is The player near enough for the activity to recieve input?
    [SerializeField] private float minTimeBetweenTasks;
    [SerializeField] private float maxTimeBetweenTasks;



    public RobotController rc;

    private static int activeTasks;
    private int count;
    private float bottomBarPlacement;
    // Start is called before the first frame update
    void Start()
    {
        onOff.SetActive(false);
        //debug, ison starts true always
        StartCoroutine(ResetTask());

        Randomize();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            ForceInput();
        }
    }


    private void ForceInput()
    {

        // move line bar.
        line.transform.localPosition = new Vector3(0, rc.currentBoostValue / 18, 0);

        // Check completion constraints

        if ((rc.currentBoostValue / 20) > bottomBarPlacement && (rc.currentBoostValue / 18) < (bottomBarPlacement + 6.0f))
        {
            count++;
            if (count >= 100.0f)
            {
                if(isOn)
                {
                    count = 0;
                    CompleteActivity();
                }
            }
        }
        print("SuccessCount [" + bottomBarPlacement + "|" + (bottomBarPlacement + 6.0f) + "]: " + count);

    }

    public void ActivateActivity()
    {
        if(activeTasks < 2)
        {
            activeTasks++;
            isOn = true;
            onOff.SetActive(true);
        }
    }

    public void CompleteActivity()
    {
        isOn = false;
        onOff.SetActive(false);
        StartCoroutine(ResetTask());
        activeTasks--;
    }

    public void Randomize()
    {
        bottomBarPlacement = Random.Range(1.0f, 3.0f);
        botBar.transform.localPosition = new Vector3(0, bottomBarPlacement, 0);
        topBar.transform.localPosition = new Vector3(0, bottomBarPlacement + 1.0f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = false;
        }
    }

    IEnumerator ResetTask()
    {
        float random = Random.Range(minTimeBetweenTasks, maxTimeBetweenTasks);
        yield return new WaitForSeconds(random);
        ActivateActivity();
    }
}
