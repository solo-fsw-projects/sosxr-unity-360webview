using System.Collections.Generic;
using UnityEngine;


public class ToggleGameObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_objectsToToggle;


    [ContextMenu(nameof(Disable))]
    public void Disable()
    {
        Toggle(false);
    }


    [ContextMenu(nameof(Enable))]
    public void Enable()
    {
        Toggle(true);
    }


    public void Toggle(bool enable)
    {
        foreach (var go in m_objectsToToggle)
        {
            go.SetActive(enable);
        }
    }
}