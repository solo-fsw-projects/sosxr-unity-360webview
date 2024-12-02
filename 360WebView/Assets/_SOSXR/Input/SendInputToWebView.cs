using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.InputSystem;
using Vuplex.WebView;


public class SendInputToWebView : MonoBehaviour
{
    [SerializeField] private CanvasWebViewPrefab m_webView;

    [SerializeField] private InputActionProperty m_inputAction;
    [SerializeField] private Key m_keyToSend = Key.A;


    private void Awake()
    {
        if (m_inputAction.action == null)
        {
            Debug.LogError("Input Action is not assigned.");

            enabled = false;

            return;
        }

        if (m_webView == null)
        {
            m_webView = FindObjectOfType<CanvasWebViewPrefab>();
        }
    }


    private void OnEnable()
    {
        m_inputAction.action.Enable();
        m_inputAction.action.performed += SendKeyOnButtonDown;
    }


    private void SendKeyOnButtonDown(InputAction.CallbackContext callbackContext)
    {
        if (m_webView == null)
        {
            this.Warning("No WebView found, cannot send key.");

            return;
        }

        m_webView.WebView.SendKey(m_keyToSend.ToString());
        this.Debug("SendKey", m_keyToSend.ToString(), "to WebView");
    }


    private void OnDisable()
    {
        m_inputAction.action.performed -= SendKeyOnButtonDown;
        m_inputAction.action.Disable();
    }
}