using System.Collections.Generic;
using UnityEngine;


namespace mrstruijk.SimpleHelpers
{
    public class ToggleGameObjectsOnAwake : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_objectsToToggle;
        [SerializeField] private bool m_stateToToggleTo = false;


        private void Awake()
        {
            foreach (var obj in m_objectsToToggle)
            {
                obj.SetActive(m_stateToToggleTo);
            }
        }
    }
}