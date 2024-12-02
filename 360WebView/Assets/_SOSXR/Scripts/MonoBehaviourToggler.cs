using System.Collections.Generic;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    public class MonoBehaviourToggler : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> m_toToggle;


        public void Toggle()
        {
            foreach (var monoBehaviour in m_toToggle)
            {
                monoBehaviour.enabled = !monoBehaviour.enabled;
            }
        }


        public void Toggle(bool enable)
        {
            foreach (var monoBehaviour in m_toToggle)
            {
                monoBehaviour.enabled = enable;
            }
        }


        public void Enable()
        {
            Toggle(true);
        }


        public void Disable()
        {
            Toggle(false);
        }
    }
}