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


    public RobotController rc;

    private int count;
    private float bottomBarPlacement;
    // Start is called before the first frame update
    void Start()
    {
        //debug, ison starts true always
        ActivateActivity();

        Randomize();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) { ForceInput(); }
    }


    private void ForceInput()
    {

        // move line bar.
        line.transform.localPosition = new Vector3(0, rc.currentBoostValue / 22, 0);

        // Check completion constraints

        if ((rc.currentBoostValue / 22) > bottomBarPlacement && (rc.currentBoostValue / 22) < (bottomBarPlacement + 1.0f))
        {
            count++;
            if (count >= 1000.0f)
            {
                CompleteActivity();
            }
        }
        print("SuccessCount [" + bottomBarPlacement + "|" + (bottomBarPlacement + 1.0f) + "]: " + count);

    }

    public void ActivateActivity()
    {
        isOn = true;
        onOff.SetActive(true);

    }

    public void CompleteActivity()
    {
        isOn = false;
        onOff.SetActive(false);

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
}
