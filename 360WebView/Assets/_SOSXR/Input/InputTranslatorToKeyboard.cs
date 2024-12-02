using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using static UnityEngine.InputSystem.InputAction;


public class InputTranslatorToKeyboard : MonoBehaviour
{
    [SerializeField] private InputActionReference m_inputActionReference;
    [SerializeField] private Key m_key = Key.A;

    private Keyboard _keyboard;
    private Key[] _keys = null;
    private KeyboardState _keyboardState;


    private void OnEnable()
    {
        if (m_inputActionReference == null || m_inputActionReference.action == null)
        {
            Debug.LogError("Input Action Reference or Action is not assigned.");

            return;
        }

        Debug.Log("Enabling Input Action.");
        m_inputActionReference.action.Enable();
        m_inputActionReference.action.performed += PerformKeyboardButtonAction;
    }


    private void Start()
    {
        Debug.Log("Initializing Keyboard and KeyboardState.");
        _keyboard = InputSystem.GetDevice<Keyboard>();

        if (_keyboard == null)
        {
            Debug.LogError("Keyboard device not found.");

            return;
        }

        _keys = new[] {m_key};
        _keyboardState = new KeyboardState(_keys);
    }


    private void PerformKeyboardButtonAction(CallbackContext obj)
    {
        Debug.Log("PerformKeyboardButtonAction CallbackContext invoked.");
        PerformKeyboardButtonAction();
    }


    [ContextMenu(nameof(PerformKeyboardButtonAction))]
    private void PerformKeyboardButtonAction()
    {
        if (_keyboard == null)
        {
            Debug.LogError("Keyboard is null, cannot queue delta state event.");

            return;
        }

        Debug.Log("Queueing Delta State Event for keyboard.");
        InputSystem.QueueDeltaStateEvent(_keyboard, _keyboardState);
        this.Debug("Input registered, translated to keyboard button", m_key.ToString());
    }


    private void OnDisable()
    {
        if (m_inputActionReference != null && m_inputActionReference.action != null)
        {
            Debug.Log("Disabling Input Action.");
            m_inputActionReference.action.performed -= PerformKeyboardButtonAction;
            m_inputActionReference.action.Disable();
        }
    }
}