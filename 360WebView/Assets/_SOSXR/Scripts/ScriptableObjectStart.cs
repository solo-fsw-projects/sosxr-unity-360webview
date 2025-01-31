using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ScriptableObjectStart : MonoBehaviour
{
    private IEnumerable<ISOStart> _sos;


    private void Start()
    {
        _sos = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<ISOStart>();

        foreach (var so in _sos)
        {
            so.SOStart();
        }
    }
}


public interface ISOStart
{
    void SOStart();
}