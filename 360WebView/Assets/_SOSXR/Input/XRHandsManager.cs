using System.Collections.Generic;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.XR;


public class XRHandsManager : MonoBehaviour
{
    public InputDevice LeftHand { get; private set; }
    public InputDevice RightHand { get; private set; }

    public bool InitialisedHands => LeftHand.isValid && RightHand.isValid;


    private void Update()
    {
        if (InitialisedHands)
        {
            return;
        }

        LeftHand = FindInputDevice(XRNode.LeftHand);

        RightHand = FindInputDevice(XRNode.RightHand);
    }


    private static InputDevice FindInputDevice(XRNode inputDevice)
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(inputDevice, devices);

        if (devices.Count == 1)
        {
            Log.Success(nameof(XRHandsManager), "We found a device: {0}, on XRNode {1}", devices[0].name,
                inputDevice.ToString());

            return devices[0];
        }

        if (devices.Count == 0)
        {
            Log.Warning(nameof(XRHandsManager), "We found no XR devices on XRNode {0}", inputDevice.ToString());

            return default;
        }

        foreach (var device in devices) // In case we have more than 1 device per XRNode
        {
            Log.Error(nameof(XRHandsManager),
                "Found too many devices on XRNode {0}, such as {1}. This shouldn't be possible?",
                inputDevice.ToString(), device.name);
        }

        return default;
    }
}