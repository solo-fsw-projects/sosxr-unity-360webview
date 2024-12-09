using UnityEngine;
using UnityEngine.Events;


public class DebugToUnityEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent m_eventToFire;
    [SerializeField] private ConfigData m_configData;


    private void OnEnable()
    {
        if (!m_configData.ShowDebug)
        {
            return;
        }

        m_eventToFire.Invoke();
    }
}