using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class MenuButtonEvent : UnityEvent<bool> { }

public class MenuButtonWatcher : MonoBehaviour
{
    public MenuButtonEvent menuButtonPress;
    public Console console;

    private bool lastButtonState = false;
    private bool appState = true;

    private List<InputDevice> devicesWithMenuButton;

    private void InputeDevices_deviceConnected(InputDevice device)
    {
        bool discardedValues;
        if (device.TryGetFeatureValue(CommonUsages.menuButton, out discardedValues))
        {
            devicesWithMenuButton.Add(device);
            console.AddLine("[" + this.name + "] add device " + device.name);
        }
    }

    private void InputeDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithMenuButton.Contains(device))
        {
            devicesWithMenuButton.Remove(device);
            console.AddLine("[" + this.name + "] remove device " + device.name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        devicesWithMenuButton = new List<InputDevice>();

        RegisterDevices();
    }

    // Update is called once per frame
    void Update()
    {
        bool tempState = false;
        foreach(var device in devicesWithMenuButton)
        {
            bool primaryButtonState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.menuButton, out primaryButtonState) && primaryButtonState || tempState;
        }

        if (tempState != lastButtonState)
        {
            console.AddLine("call menuButtonPress with " + tempState);
            if (tempState)
            {
                appState = !appState;
                menuButtonPress.Invoke(appState);
            }
            lastButtonState = tempState;
        }
    }

    private void OnEnable()
    {
        RegisterDevices();
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputeDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputeDevices_deviceDisconnected;
        devicesWithMenuButton.Clear();
    }

    private void RegisterDevices()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputeDevices_deviceConnected(device);
        InputDevices.deviceConnected += InputeDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputeDevices_deviceDisconnected;
    }
}
