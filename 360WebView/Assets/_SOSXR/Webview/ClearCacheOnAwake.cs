using UnityEngine;
using Vuplex.WebView;


public class ClearCacheOnAwake : MonoBehaviour
{
    [SerializeField] private bool m_clearCacheOnAwake = true;


    private void Awake()
    {
        if (!m_clearCacheOnAwake)
        {
            return;
        }

        Web.ClearAllData();
    }
}