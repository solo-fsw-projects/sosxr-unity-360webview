using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ScriptableObjectUpdate : MonoBehaviour
{
    private IEnumerable<ISOUpdate> _sos;


    private void Awake()
    {
        _sos = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<ISOUpdate>();
    }


    private void Update()
    {
        foreach (var so in _sos)
        {
            so.SOUpdate();
        }
    }
}


public interface ISOUpdate
{
    void SOUpdate();
}