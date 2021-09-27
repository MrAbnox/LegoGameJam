using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LEGODeviceUnitySDK;
using Unity.LEGO.Utilities;
using UnityEngine.EventSystems;
using System.Linq;

public class GameManager : MonoBehaviour, ILEGOGeneralServiceDelegate
{
    public static GameManager instance;

    ILEGODevice device;
    private bool isConnectedToHub;
    private float currentRollValue = 0;
    private float currentPitchValue = 0;
    private float currentBoostValue = 0;
    float currentColor;

    private bool isMotor;

    LEGOTechnicMotor pitchMotor;
    LEGOTechnicMotor rollMotor;
    LEGORGBLight rgbLight;

    LEGOTechnicForceSensor forceSensor;

    LEGOColorSensor colorSensor;
    LEGOTechnicDistanceSensor technicDistanceSensor;

    LEGOTechnicColorSensor technicColorSensor;
    LEGOVisionSensor visionSensor;

    public ILEGODevice Device { get => device; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("There's one too many GameManagers in the Scene");
            Destroy(this);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetUpWithDevice(ILEGODevice device)
    {

        this.device = device;
        Debug.LogFormat("Setting up light");
        var lightServices = ServiceHelper.GetServicesOfType(device, IOType.LEIOTypeRGBLight);
        if (lightServices == null || lightServices.Count() == 0)
        {
            Debug.LogFormat("No light services found!");
        }
        else
        {
            rgbLight = (LEGORGBLight)(lightServices.First());
            Debug.LogFormat("Has light service {0}", rgbLight);
            rgbLight.UpdateCurrentInputFormatWithNewMode((int)LEGORGBLight.RGBLightMode.Discrete);
        }


        Debug.LogFormat("Setting roll motor"); // Must be connected to port A.
        var rollMotors = ServiceHelper.GetServicesOfTypeOnPort(device, IOType.LEIOTypeTechnicMotorXL, 0);
        if (rollMotors == null || rollMotors.Count() == 0)
        {
            Debug.LogFormat("No rollMotors found!");
        }
        else
        {
            isMotor = true;
            rollMotor = (LEGOTechnicMotor)rollMotors.First();
            Debug.LogFormat("Has motor service {0}", rollMotor);
            rollMotor.UpdateInputFormat(new LEGOInputFormat(rollMotor.ConnectInfo.PortID, rollMotor.ioType, rollMotor.PositionModeNo, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            rollMotor.RegisterDelegate(this);
        }

        Debug.LogFormat("Setting pitch motor");// Must be connected to port C.
        var pitchMotors = ServiceHelper.GetServicesOfTypeOnPort(device, IOType.LEIOTypeTechnicMotorXL, 2);
        if (pitchMotors == null || pitchMotors.Count() == 0)
        {
            Debug.LogFormat("No pitchMotors found!");
        }
        else
        {
            pitchMotor = (LEGOTechnicMotor)pitchMotors.First();
            Debug.LogFormat("Has motor service {0}", pitchMotor);
            pitchMotor.UpdateInputFormat(new LEGOInputFormat(pitchMotor.ConnectInfo.PortID, pitchMotor.ioType, pitchMotor.PositionModeNo, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            pitchMotor.RegisterDelegate(this);
            var cmd = new LEGOTachoMotorCommon.SetSpeedPositionCommand()
            {
                Position = 0,
                Speed = 1
            };
            cmd.SetEndState(MotorWithTachoEndState.Drifting);
            pitchMotor.SendCommand(cmd);
        }

        var forceSensorServices = ServiceHelper.GetServicesOfType(device, IOType.LEIOTypeTechnicForceSensor);
        if (forceSensorServices == null || forceSensorServices.Count() == 0)
        {
            Debug.LogFormat("No force sensor services found   !");
        }
        else
        {
            forceSensor = (LEGOTechnicForceSensor)(forceSensorServices.First());
            Debug.LogFormat("Has forceSensor service {0}", forceSensor);
            // Mode 0 - Variable force
            // Mode 1 - Binary pressed/not pressed
            // Mode 2 - not sure what this is...
            int mode = 0;
            forceSensor.UpdateInputFormat(new LEGOInputFormat(forceSensor.ConnectInfo.PortID, forceSensor.ioType, mode, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            forceSensor.RegisterDelegate(this);
        }
        //new by mahmoud
        var visionSensorServises = ServiceHelper.GetServicesOfType(device, IOType.LEIOTypeVisionSensor);
        if (visionSensorServises == null || visionSensorServises.Count() == 0)
        {
            Debug.LogFormat("No vision sensor services found ! ");
        }
        else
        {
            visionSensor = (LEGOVisionSensor)(visionSensorServises.First());
            Debug.LogFormat("Has visionSensor service {0}", visionSensor);

            visionSensor.UpdateInputFormat(new LEGOInputFormat(visionSensor.ConnectInfo.PortID, visionSensor.ioType, 0, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            visionSensor.RegisterDelegate(this);
        }

        var ColorSensorServises = ServiceHelper.GetServicesOfType(device, IOType.LEIOTypeTechnicColorSensor);
        if (ColorSensorServises == null || ColorSensorServises.Count() == 0)
        {
            Debug.LogFormat("No Color Distance sensor services found ! ");
        }
        else
        {
            technicColorSensor = (LEGOTechnicColorSensor)(ColorSensorServises.First());
            Debug.LogFormat("Has Color Distance Sensor service {0}", technicColorSensor);

            technicColorSensor.UpdateInputFormat(new LEGOInputFormat(technicColorSensor.ConnectInfo.PortID, technicColorSensor.ioType, 0, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            technicColorSensor.RegisterDelegate(this);
        }
        //end

        isConnectedToHub = true;
    }

    public void DidUpdateValueData(ILEGOService service, LEGOValue oldValue, LEGOValue newValue)
    {
        if (service == pitchMotor)
        {
            currentPitchValue = newValue.RawValues[0];
        }
        else if (service == rollMotor)
        {
            currentRollValue = newValue.RawValues[0];
        }
        else if (service == forceSensor)
        {
            currentBoostValue = newValue.RawValues[0];
        }
        else if (service == visionSensor)
        {
            // currentColor = newValue.;
            // currentColor3 = newValue.RawData[0];
            // currentColor = newValue.RawValues[0];
            // currentColor2 = newValue.RawValuesToString();
            // currentColor2 = newValue.ModeName;
            // mm = "false";
        }
        else if (service == technicColorSensor)
        {
            currentColor = newValue.RawValues[0];
        }
    }

    public void DidUpdateMeasuredColorFrom(LEGOColorSensor colorSensor, LEGOValue oldColorIndex, LEGOValue newColorIndex)
    {
        Debug.LogFormat("DidUpdateColorIndexFrom {0} {1}", newColorIndex.RawValues, newColorIndex.RawValues.Length);
        var currentColor4 = newColorIndex.RawValues;
        print(currentColor4);
        // /colorIndicator.color = _defaultColorSet[(int)newColorIndex.RawValues[0]];
    }

    public void DidChangeState(ILEGOService service, ServiceState oldState, ServiceState newState)
    {
        //    Debug.LogFormat("DidChangeState {0} to {1}", service, newState);
    }

    public void DidUpdateInputFormat(ILEGOService service, LEGOInputFormat oldFormat, LEGOInputFormat newFormat)
    {
        //    Debug.LogFormat("DidUpdateInputFormat {0} to {1}", service, newFormat);
    }

    public void DidUpdateInputFormatCombined(ILEGOService service, LEGOInputFormatCombined oldFormat, LEGOInputFormatCombined newFormat)
    {
        //    Debug.LogFormat("DidUpdateInputFormatCombined {0} to {1}", service, newFormat);
    }
}
