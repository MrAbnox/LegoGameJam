using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LEGODeviceUnitySDK;

public class RobotGame : MonoBehaviour
{
    [SerializeField] bool connectToBLE;
    public DeviceHandler deviceHandler;
    RobotController robotController;

    // Start is called before the first frame update
    void Start()
    {
        robotController = GetComponent<RobotController>();
        deviceHandler.OnDeviceInitialized += OnDeviceInitialized;
        if (connectToBLE == true)
            deviceHandler.AutoConnectToDeviceOfType(HubType.Technic);
    }

    public void OnDeviceInitialized(ILEGODevice device)
    {
        Debug.LogFormat("OnDeviceInitialized {0}", device);
        robotController.SetUpWithDevice(device);
    }
}
