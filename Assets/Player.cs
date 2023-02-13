using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class MenuButtonEvent : UnityEvent<bool> { }

public class Player : MonoBehaviour
{
    public XRNode controller;

    public MenuButtonEvent menuButtonPress;
    private bool lastButtonState = false;

    public Inventory inventory;
    public int itemSelected;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controller);
        bool state;
        device.TryGetFeatureValue(CommonUsages.menuButton, out state);

        if (state != lastButtonState)
        {
            menuButtonPress.Invoke(state);
            lastButtonState = state;
        }
    }
}
