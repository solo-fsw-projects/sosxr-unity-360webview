using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ScriptableObjectOnDisable : MonoBehaviour
{
    private IEnumerable<ISOOnDisable> _sos;


    private void Awake()
    {
        _sos = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<ISOOnDisable>();
    }


    private void OnDisable()
    {
        foreach (var so in _sos)
        {
            so.SOOnDisable();
        }
    }
}


public interface ISOOnDisable
{
    void SOOnDisable();
}