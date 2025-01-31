using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ScriptableObjectAwake : MonoBehaviour
{
    private IEnumerable<ISOAwake> _sos;


    private void Awake()
    {
        _sos = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<ISOAwake>();

        foreach (var so in _sos)
        {
            so.SOAwake();
        }
    }
}


public interface ISOAwake
{
    public void SOAwake();
}