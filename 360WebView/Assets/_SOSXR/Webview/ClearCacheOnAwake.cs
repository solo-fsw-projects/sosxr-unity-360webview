using UnityEngine;
using Vuplex.WebView;


public class ClearCacheOnAwake : MonoBehaviour
{
    private void Awake()
    {
        Web.ClearAllData();
    }
}