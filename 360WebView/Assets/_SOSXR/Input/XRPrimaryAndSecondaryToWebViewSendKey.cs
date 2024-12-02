using SOSXR.EnhancedLogger;
using UnityEngine;
using Vuplex.WebView;
using CommonUsages = UnityEngine.XR.CommonUsages;


public class XRPrimaryAndSecondaryToWebViewSendKey : MonoBehaviour
{
    [SerializeField] private XRHandsManager m_xrHandsManager;
    [SerializeField] private CanvasWebViewPrefab m_webViewPrefab;

    [Header("Translate primary buttons to keyboard input")]
    [Tooltip("On Quest and Pico models this button is marked A")]
    [SerializeField] private string m_rightPrimary = "A";
    [Tooltip("On Quest and Pico models this button is marked X")]
    [SerializeField] private string m_leftPrimary = "X";

    [Header("Translate secondary buttons to keyboard input")]
    [Tooltip("On Quest and Pico models this button is marked B")]
    [SerializeField] private string m_rightSecondary = "B";
    [Tooltip("On Quest and Pico models this button is marked Y")]
    [SerializeField] private string m_leftSecondary = "Y";

    private bool _buttonDown;


    private void Update()
    {
        if (!m_xrHandsManager.InitialisedHands)
        {
            return;
        }

        bool tempValue;

        if (m_xrHandsManager.RightHand.TryGetFeatureValue(CommonUsages.primaryButton, out tempValue) && tempValue)
        {
            SendKeyOnButtonDown(m_rightPrimary);
        }
        else if (m_xrHandsManager.LeftHand.TryGetFeatureValue(CommonUsages.primaryButton, out tempValue) && tempValue)
        {
            SendKeyOnButtonDown(m_leftPrimary);
        }
        else if (m_xrHandsManager.RightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out tempValue) &&
                 tempValue)
        {
            SendKeyOnButtonDown(m_rightSecondary);
        }
        else if (m_xrHandsManager.LeftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out tempValue) && tempValue)
        {
            SendKeyOnButtonDown(m_leftSecondary);
        }
        else
        {
            AllKeysDepressed();
        }
    }


    private void SendKeyOnButtonDown(string keyToSend)
    {
        if (_buttonDown)
        {
            return;
        }

        m_webViewPrefab.WebView.SendKey(keyToSend);
        this.Debug("SendKey", keyToSend);

        _buttonDown = true;
    }


    private void AllKeysDepressed()
    {
        if (!_buttonDown)
        {
            return;
        }

        this.Debug("All buttons are now depressed");
        _buttonDown = false;
    }
}