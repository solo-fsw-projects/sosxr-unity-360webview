using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class InputToUnityEvent : MonoBehaviour
{
    [SerializeField] private InputActionProperty m_inputAction;
    [SerializeField] private UnityEvent m_eventToFire;


    private void OnEnable()
    {
        m_inputAction.action.Enable();
        m_inputAction.action.performed += InvokeEvent;
    }


    private void InvokeEvent(InputAction.CallbackContext obj)
    {
        InvokeEvent();
    }


    [ContextMenu(nameof(InvokeEvent))]
    private void InvokeEvent()
    {
        m_eventToFire?.Invoke();
        this.Debug("Input registered, firing event.");
    }


    private void OnDisable()
    {
        m_inputAction.action.performed -= InvokeEvent;
        m_inputAction.action.Disable();
    }
}